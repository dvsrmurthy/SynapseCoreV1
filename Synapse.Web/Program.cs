using ClientHTTPConsuming.Utilities;
using Core.Data.Data.Account;
using Core.Data.Data.Services;
using Core.Data.Data.Synapse;
using Core.Data.IDataInterfaces.Account;
using Core.Data.IDataInterfaces.ISynapse;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using Synapse.Web.Helpers;
using Synapse.Web.Helpers.SecureAccess;
using SynapseAPI.Controllers;
using SynapseAPI.Models;

var builder = WebApplication.CreateBuilder(args);

AESEncrytDecry.Initialize(builder.Configuration);
AppInternalEncKey.Initialize(builder.Configuration);
AppConfiguration.Initialize(builder.Configuration);
var synapseApiBaseUrl = builder.Configuration["BaseServiceHostUrl"];
builder.Services.AddScoped<ISerializer, ClientHTTPConsuming.Utilities.Serializers.JsonSerializer>();
builder.Services.AddScoped<IRestRequest, RestRequest>();

builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(typeof(AccountController).Assembly)
    .AddApplicationPart(typeof(SynapseAPI.Controllers.DivisionsController).Assembly);

builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(
        typeof(Synapse.Web.CampaignPlugin.Controllers.CampaignPluginController).Assembly);
    //.AddApplicationPart(
    //    typeof(Synapse.Web.AlertsPlugin.Controllers.AlertsPluginController)
    //        .Assembly);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
// Add authentication and authorization services - For Forms Authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization();

// Add authentication and authorization services - For Forms Authentication
builder.Services.AddScoped<IAccountCoreData, AccountCoreData>();
builder.Services.AddScoped<ISynapseCoreData, SynapseCoreData>();
builder.Services.AddScoped<ThirdPartyServiceConsumption>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddSynapseApiServices(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapControllers();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".pdf"] = "application/pdf";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.Run();

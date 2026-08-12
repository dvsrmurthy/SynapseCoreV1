using APIServices.Helpers;
using Core.Data.Data.Account;
using Core.Data.Data.Synapse;
using Core.Data.IDataInterfaces.Account;
using Core.Data.IDataInterfaces.ISynapse;
using SynapseAPI.Controllers;
using System;
using System.Configuration;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
AppConfigurationAPI.Initialize(builder.Configuration);
Core.Utilities.Helpers.AppInternalEncKey.Initialize(builder.Configuration);
Core.Data.Data.Account.AccountCoreData.Initialize(builder.Configuration);
Core.Data.Utilities.CoreDBConsumer.Initialize(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// after building builder
builder.Services.AddSingleton<ISynapseCoreData, SynapseCoreData>();
builder.Services.AddSingleton<IAccountCoreData, AccountCoreData>();
builder.Services.AddScoped<ValidateBasicAuthrioze>();
// or use AddScoped/AddTransient depending on lifetime requirements

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

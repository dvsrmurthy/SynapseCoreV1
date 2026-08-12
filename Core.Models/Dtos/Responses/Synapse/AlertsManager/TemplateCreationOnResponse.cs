using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AlertsManager
{
    public class GetTemplateDetailsResponse
    {
        public int TempId { get; set; }
        public string? TemplateName { get; set; }
        public string? Description { get; set; }
        public string? Text { get; set; }
        public int Language { get; set; }
        public int Type { get; set; }
        public bool TempStatus { get; set; }
        public string? CreatedByUser { get; set; }
        public string? CustomerName { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
        public int CurrentTempStatus { get; set; }
        //public string? RejectNote { get; set; }
        public int TemplateId { get; set; }
       // public string? Columns { get; set; }
        public string? columns { get; set; }
        // public int Id { get; set; }
        public bool Status { get; set; }
        // public int CreatedBy { get; set; }
        public int SMSTemplate { get; set; }
        public int EMAILTemplate { get; set; }
        public string? TextEditor { get; set; }
        public string? RejectNote { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
    }

    public class gettemplatecolumnsresponse
    {
        public int id { get; set; }
        public string? columns { get; set; }
    }

    public class gettemplatecolumnsresponseedit
    {
        public int id { get; set; }
        public string? columns { get; set; }
    }


    public class GetTemplateDetailsResponseforedit
    {
        public int TempId { get; set; }
        public string? TemplateName { get; set; }
        public string? Description { get; set; }
        public string? Text { get; set; }
        public int Language { get; set; }
        public int Type { get; set; }
        public bool TempStatus { get; set; }
        public string? CreatedByUser { get; set; }
        public string? CustomerName { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
        public int CurrentTempStatus { get; set; }
        //public string? RejectNote { get; set; }
        public int TemplateId { get; set; }
        public string? columns { get; set; }
        // public int Id { get; set; }
        public bool Status { get; set; }
        // public int CreatedBy { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
    }
    #region UserMappings

    public class GetTemplateUserMapDetailsResponse
    {
        public int TemplateId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int Status { get; set; }
        public string? TemplateName { get; set; }       
        public string? CustomerName { get; set; }
        public string? UserName { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectNote { get; set; }
        public string? CreatedByUser { get; set; }
    }
    public class GetCustomersDetailsResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CustomerType { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
       // public DateTime ExpiryDate { get; set; }
    }
    public class GetUsersDetailsResponse
    {
        public int Userid { get; set; }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Mail { get; set; }
        public string? MobileNo { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int divisionid { get; set; }
        public string? DivisionName { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public int Level { get; set; }
        public bool Http { get; set; }
        public bool Smtp { get; set; }
        public bool Web { get; set; }
        public bool Smpp { get; set; }
        public int SessionsCount { get; set; }
        public bool Status { get; set; }
        public string? NAME { get; set; }
        public string? DecryptPassword { get; set; }
        public string? Customer { get; set; }
        public int Fstatus { get; set; }
        public bool ldap { get; set; }
        public string? RejectNote { get; set; }

    }
    #endregion
}

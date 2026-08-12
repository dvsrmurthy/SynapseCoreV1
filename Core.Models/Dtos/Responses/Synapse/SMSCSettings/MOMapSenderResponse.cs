using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
  public class MOMapSenderResponse
    {
      public string? Id { get; set; }
      public int CustomerId { get; set; }
      public int UserId { get; set; }
      public int ShortCodeId { get; set; }
      public string? DisplayShortCode { get; set; }
      public int ShortCodeType { get; set; }
      public bool Status { get; set; }
      public string? SavedOn { get; set; }
      public string? UpdatedON { get; set; }
      public int CreatedBy { get; set; }
      public string? CustomerName { get; set; }
      public string? UserName { get; set; }
      public string? ShortCodeName { get; set; }
    }
}

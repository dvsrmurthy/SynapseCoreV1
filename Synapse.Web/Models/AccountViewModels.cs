using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Synapse.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string loginCaptcha { get; set; }
        public string ActionVerificationId { get; set; }

        public string VerifiedToken { get; set; }

        public string usercheck { get; set; }
        public string Otp { get; set; }
        public string forgotPasswordOtp { get; set; }
        public string forgotPasswordUsername { get; set; }
        public string forgotPasswordEmail { get; set; }
        public string LoginCaptchaValue { get; set; }
        public string ForgotPasswordCaptchaValue { get; set; }
        public string WhiteListIPCaptchaValue { get; set; }
        public string forgotPasswordrandomcapt { get; set; }
        public string forgotForm { get; set; } = "0";
        public string randomcapt { get; set; }
        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
        public string radiocheck { get; set; }
        public int emailotp { get; set; }
        public string Labelname { get; set; }
        public string Hidlabel { get;set; }
        public string Mename { get; set; }
        public string sixdigits { get; set; }
        public string forgotReturnMessage { get; set; }

        public string ipwhitelistUserName { get; set; }
        public string ipwhitelistPassword { get; set; }
        public string ipwhitelistRandomcapt { get; set; }
        public string ipwhitelistMobileOTP { get; set; }
        public string MobileNo { get; set; }   
        public LoginViewModelAdditionalData loginViewModelAdditionalData { get; set; }
    }
    public class LoginViewModelAdditionalData
    {
        public string invalidCaptcha { get; set; }
        public string Username { get; set; }
        public string Captch { get; set; }
        public string Message { get; set; }
        public bool IsOTPSent { get; set; }
        public int FormToDisplay { get; set; }       
        public string ActionResult { get; set; }
        public string MobileNo { get; set; }
        public string Email { get;set; }
        public string Pword { get; set; }
        public int Otptime { get; set; }
        public string verifymessage { get; set; }   
        public int userId { get; set; } 
        public int customerId { get; set; } 
        public string otpmessage { get; set; }
        public string RandValue { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ReceoveryPasswordViewModel
    {        
        public string username { get; set; }
        public string otp { get; set; }
        public string newPassword { get; set; }
        public int userid { get; set; }
        public string result { get; set; }

    }
}

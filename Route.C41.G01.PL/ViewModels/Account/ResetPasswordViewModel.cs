using System.ComponentModel.DataAnnotations;

namespace Route.C41.G01.PL.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "NewPassword is Requird")]
        [MinLength(5, ErrorMessage = "Minimum password length is [5] digits")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }



        [Required(ErrorMessage = "Password is Requird")]
        [MinLength(5, ErrorMessage = "Minimum password length is [5] digits")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match with password")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Route.C41.G01.PL.ViewModels.User
{
    public class SignUpViewModel
    {
		[Required(ErrorMessage ="UserName is Required")]
		public string UserName { get; set; }

        [Required(ErrorMessage ="Email is Requird")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage="First Name is Required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }


        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is Requird")]
        [MinLength(5,ErrorMessage ="Minimum password length is [5] digits")]
        [DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Password is Requird")]
		[MinLength(5, ErrorMessage = "Minimum password length is [5] digits")]
		[DataType(DataType.Password)]
        [Compare(nameof(SignUpViewModel.Password), ErrorMessage ="Confirm Password doesn't match with password")]
		public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PL_Proj.ViewModels
{
	public class ForgetPassword
	{
		[
			Required(ErrorMessage = "Email Is Required"),
			EmailAddress(ErrorMessage = "Invalid Email")
		]
		public string Email { get; set; }
	}

}

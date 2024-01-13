using System.ComponentModel.DataAnnotations;

namespace PL_Proj.ViewModels
{
	public class ResetPasswordViewModel
	{
		[
			Required(ErrorMessage = "Password Is Required"),
			DataType(DataType.Password)
		]
		public string NewPassword { get; set; }
		[
			Required(ErrorMessage = "Confirm Password Is Required"),
			DataType(DataType.Password),
			Compare("NewPassword", ErrorMessage = "Password Doesn't Match")
		]
		public string ConfirmPassword { get; set; }
	}
}

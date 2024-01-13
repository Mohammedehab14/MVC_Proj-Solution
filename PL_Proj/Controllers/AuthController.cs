using DAL_Proj.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PL_Proj.Utilities;
using PL_Proj.ViewModels;
using System.Threading.Tasks;

namespace PL_Proj.Controllers
{
    public class AuthController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region Sign Up
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel item)
        {
            
            if(ModelState.IsValid )
            {
                var user = await _userManager.FindByNameAsync(item.UserName);
                if (user == null)
                {
					user = new AppUser()
					{
						UserName = item.UserName,
						Email = item.Email,
						FName = item.FName,
						LName = item.LName,
						IsAgree = item.IsAgree,
					};
					var Result = await _userManager.CreateAsync(user, item.Password);
					if (Result.Succeeded)
						return RedirectToAction("SignIn");
					else
						foreach (var Err in Result.Errors)
							ModelState.AddModelError(string.Empty, Err.Description);
				}
				ModelState.AddModelError(string.Empty, "UserName Is Already Exists");
            }
            return View(item);
        }
		#endregion

		#region Sign In
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel item)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(item.Email);
				if(user != null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, item.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, item.Password, item.RememberMe, false);
						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(item);
		}
		#endregion

		#region Sign Out
		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		#region Forget Password
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendResetPasswordURL(ForgetPassword item)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(item.Email);
				if (user != null)
				{
					var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var ResetPassURL = Url.Action("ResetPassword", "Auth", new { email = item.Email, Token }, Request.Scheme);
					var email = new Email()
					{
						Subject = "Reset Password",
						Recipient = item.Email,
						Body = ResetPassURL
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Email Isn't Exist");
			}
			return View("ForgetPassword", item);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region Reset Password
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel item)
		{
			if (ModelState.IsValid)
			{
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				if (user != null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, item.NewPassword);
					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));
					else
						foreach (var err in result.Errors)
							ModelState.AddModelError(string.Empty, err.Description);
				}
			}
			return View(item);
		}
		#endregion
	}
}

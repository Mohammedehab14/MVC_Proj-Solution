using AutoMapper;
using DAL_Proj.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL_Proj.Utilities;
using PL_Proj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL_Proj.Controllers
{
    [Authorize]
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
		{
			if(string.IsNullOrEmpty(searchValue))
			{
				var users = await _userManager.Users.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FName = u.FName,
					LName = u.LName,
					Email = u.Email,
					PhoneNumber = u.PhoneNumber,
					Roles = _userManager.GetRolesAsync(u).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(searchValue);
				var MappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel> { MappedUser });
			}
		}

		public async Task<IActionResult> Details(string id, string viewName = "Details")
		{
			if (id == null)
				return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
			if(user == null)
				return NotFound();
			var MappedUser = _mapper.Map<AppUser,UserViewModel>(user);
			return View(viewName, MappedUser);
		}

        public async Task<IActionResult> Edit(string Id)
        {


            return await Details(Id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel user, [FromRoute] string Id)
        {
            if (Id != user.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var resultUser = await _userManager.FindByIdAsync(Id);
                    resultUser.FName = user.FName;
                    resultUser.LName = user.LName;
                    resultUser.PhoneNumber = user.PhoneNumber;
					await _userManager.UpdateAsync(resultUser);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(UserViewModel item, [FromRoute] string id)
        {
            
            try
            {
                var user = await _userManager.FindByIdAsync(item.Id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }


}

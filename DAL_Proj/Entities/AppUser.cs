using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Proj.Entities
{
	public class AppUser : IdentityUser
	{
		[Required]
		public string FName { get; set; }
		[Required]
		public string LName { get; set; }
		public bool IsAgree { get; set; }
    }
}

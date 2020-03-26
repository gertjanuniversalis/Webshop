using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using static WebshopMVC.Domain.Entities.Constants;

namespace WebshopMVC.Domain.Entities
{
	public class User
	{
		[HiddenInput(DisplayValue = false)]
		public int ID { get; set; }

		[Required(ErrorMessage = "Provide a name")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Provide an email address")]
		public string Email { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Password { get; set; }
		//public EUserRoles[] Roles { get; set; } = new EUserRoles[] { 0 };
	}
}

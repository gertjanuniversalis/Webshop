using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebshopMVC.WebUI.Models
{
	public class LoginViewModel
	{
		[Required (ErrorMessage = "Provide your email")]
		public string Email { get; set; }

		[Required (ErrorMessage = "Enter your password")]
		public string Password { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebshopMVC.WebUI.Models
{
	public class SignUpViewModel
	{
		[Required (ErrorMessage = "Enter your name")]
		public string UserName { get; set; }

		[Required (ErrorMessage = "Enter a valid email")]
		public string Email { get; set; }

		[Required (ErrorMessage = "Choose a password")]
		public string PassWord { get; set; }

		[Required (ErrorMessage = "Repeat your password")]
		public string repeatPassWord { get; set; }
	}
}
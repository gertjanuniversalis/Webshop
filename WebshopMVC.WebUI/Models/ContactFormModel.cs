using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebshopMVC.WebUI.Models
{
	public class ContactFormModel
	{
		[Required(ErrorMessage = "Enter your username")]
		public string UserName { get; set; }

		[Required(ErrorMessage ="Enter a response address")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Enter a message")]
		public string Message { get; set; }
	}
}
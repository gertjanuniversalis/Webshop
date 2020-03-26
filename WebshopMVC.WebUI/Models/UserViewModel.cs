using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebshopMVC.Domain.Entities;

namespace WebshopMVC.WebUI.Models
{
	public class UserViewModel
	{
		private User UserToken;

		public User StoredUser => UserToken;

		public UserViewModel(User user)
		{
			this.UserToken = user;
		}
	}
}
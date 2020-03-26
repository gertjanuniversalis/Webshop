using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;
using WebshopMVC.Domain.Infrastructure.Abstract;
using WebshopMVC.WebUI.Infrastructure.Abstract;
using WebshopMVC.WebUI.Models;

namespace WebshopMVC.WebUI.Infrastructure.Concrete
{
	public class UserAuthenticator : IUserAuthenticator
	{
		private IAuthorisation Authorizer;

		public UserAuthenticator(IAuthorisation authorisation)
		{
			Authorizer = authorisation;
		}

		public User CanLogin(LoginViewModel loginModel)
		{
			User user = new User { Email = loginModel.Email, Password = loginModel.Password };

			return Authorizer.LoginAuthorised(user);
		}
	}
}
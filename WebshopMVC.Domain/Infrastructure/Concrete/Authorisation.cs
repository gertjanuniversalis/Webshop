using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;
using WebshopMVC.Domain.Infrastructure.Abstract;
using static WebshopMVC.Domain.Entities.Constants;

namespace WebshopMVC.Domain.Infrastructure.Concrete
{
	public class Authorisation : IAuthorisation
	{
		private IUserRepository Userrepository;

		
		public Authorisation(IUserRepository userrepository)
		{
			Userrepository = userrepository;
		}
		
		private bool ContainsRole(List<EUserRoles> userRoles, List<EUserRoles> allowedRoles)
		{
			return userRoles.Any(x => allowedRoles.Any(y => y == x));
		}

		private List<EUserRoles> GetUserRoles(int userID)
		{
			return Userrepository.GetUserRoles(userID);
		}

		public bool CanModifyUser(int UserToModify, User loginToken)
		{
			if (loginToken.ID == UserToModify)
			{
				//A user may always modify himself
				return true;
			}
			else
			{
				List<EUserRoles> userRoles = GetUserRoles(loginToken.ID);

				bool hasMatch =ContainsRole(userRoles, Constants.CanModifyUser);

				return hasMatch;
			}
		}

		public bool CanUploadProduct(User loginToken)
		{	
			List<EUserRoles> userRoles = GetUserRoles(loginToken.ID);

			bool hasMatch = ContainsRole(userRoles, Constants.CanUploadProduct);

			return hasMatch;
		}

		public bool CanEditProduct(User userToken)
		{
			List<EUserRoles> userRoles = GetUserRoles(userToken.ID);

			bool hasMatch = ContainsRole(userRoles, Constants.CanEditProduct);

			return hasMatch;
		}

		public bool CanDeleteProduct(User loginToken)
		{

			List<EUserRoles> userRoles = Userrepository.GetUserRoles(loginToken.ID);

			bool hasMatch = userRoles.Any(x => Constants.CanDeleteProduct.Any(y => y == x));

			return hasMatch;
		}

		public User LoginAuthorised(User loginCredentials)
		{
			User registeredUser = Userrepository.GetUserByEmail(loginCredentials.Email);

			if (registeredUser != null)
			{
				if (loginCredentials.Password == registeredUser.Password)
				{
					//clear the password field to prevent extraction, then return
					return new User
					{
						Name = registeredUser.Name,
						ID = registeredUser.ID,
						Email = registeredUser.Email
					};
				}
			}
			return null;
		}
	}
}

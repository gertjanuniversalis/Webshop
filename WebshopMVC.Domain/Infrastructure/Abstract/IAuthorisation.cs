using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Infrastructure.Abstract
{
	public interface IAuthorisation
	{
		/// <summary>
		/// Checks if the referenced user is allowed to login.
		/// </summary>
		/// <param name="user"></param>
		/// <returns>A "User" object to store in-session if authorised, or null</returns>
		User LoginAuthorised(User user);

		bool CanUploadProduct(User loginToken);
		bool CanDeleteProduct(User loginToken);
		bool CanModifyUser(int UserToModify, User loginToken);
		bool CanEditProduct(User userToken);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Abstract
{
	public interface IUserRepository
	{
		/// <summary>
		/// Gets a user from the database based on their ID
		/// </summary>
		User GetUserByID(int userID);
		/// <summary>
		/// Gets a user from the database based on their email
		/// </summary>
		User GetUserByEmail(string email);

		/// <summary>
		/// Creates a new user in the database, returning their assigned ID
		/// </summary>
		int CreateUser(User user);

		/// <summary>
		/// Attempts to remove a user from the database, based on the ID
		/// </summary>
		bool RemoveUser(int userID);
		/// <summary>
		/// Attempts to remove a user from the database, based on their email
		/// </summary>
		bool RemoveUser(string email);

		/// <summary>
		/// Updates a user to a new state
		/// </summary>
		bool ModifyUser(User user);

		/// <summary>
		/// Lists the roles assigned to this user
		/// </summary>
		List<Constants.EUserRoles> GetUserRoles(int userID);
	}
}

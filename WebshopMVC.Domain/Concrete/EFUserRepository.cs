using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Concrete
{
	public class EFUserRepository : IUserRepository
	{
		private EFDbContext Context = new EFDbContext();

		public IEnumerable<User> Users
		{
			get { return Context.Users; }
		}

		public int CreateUser(User user)
		{
			User existingEntry = Context.Users.Where(u => u.Email == user.Email).FirstOrDefault();
			if (existingEntry != null)
			{
				return -1;
			}
			else
			{
				Context.Users.Add(user);				
				Context.SaveChanges();

				User dbEntry = Context.Users.Where(u => u.Email == user.Email).FirstOrDefault();

				return dbEntry == null ? -1 : dbEntry.ID;
			}
		}

		public User GetUserByEmail(string email)
		{
			if (email.Length == 0)
			{
				return null;
			}
			else
			{
				User user = Context.Users.Where(u=>u.Email == email).FirstOrDefault();
				return user;
			}
		}

		public User GetUserByID(int userID)
		{
			if (userID < 0)
			{
				return null;
			}
			else
			{
				User user = Context.Users.Find(userID);
				return user;
			}
		}

		public List<Constants.EUserRoles> GetUserRoles(int userID)
		{
			List<int> roleIDs = Context.UserRoles.Where(ur => ur.UserID == userID).Select(ur => ur.RoleID).ToList();

			List<Constants.EUserRoles> roleNames = new List<Constants.EUserRoles>();

			foreach(int id in roleIDs)
			{
				roleNames.Add((Constants.EUserRoles)id);
			}

			return roleNames;
		}

		public bool ModifyUser(User user)
		{
			if (user.ID >= 0)
			{
				User dbEntry = Context.Users.Find(user.ID);
				if (dbEntry != null)
				{
					dbEntry.Name = user.Name;
					dbEntry.Email = user.Email;
					dbEntry.Password = user.Password;

					Context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public bool RemoveUser(int userID)
		{
			User dbEntry = GetUserByID(userID);

			return RemoveUser(dbEntry);
		}

		public bool RemoveUser(string email)
		{
			User dbEntry = GetUserByEmail(email);

			return RemoveUser(dbEntry);
		}

		private bool RemoveUser(User user)
		{
			if (user != null)
			{
				Context.Users.Remove(user);
				Context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool EditUserRoles(int userID, List<UserRole> newRoles)
		{
			try
			{
				List<int> existingRoles = Context.UserRoles.Where(ur => ur.UserID == userID).Select(ur => ur.RoleID).ToList();
				List<int> newRoleIDs = newRoles.Select(nr => nr.RoleID).ToList();

				foreach (int existingID in existingRoles)
				{
					if (!newRoleIDs.Contains(existingID))
					{
						UserRole delRole = Context.UserRoles.Where(ur => ur.UserID == userID && ur.RoleID == existingID).FirstOrDefault();
						if (delRole != null)
						{
							Context.UserRoles.Remove(delRole);
							newRoleIDs.Remove(existingID);
						}
					}
				}

				foreach (int newRole in newRoleIDs)
				{
					Context.UserRoles.Add(
						new UserRole
						{
							UserID = userID,
							RoleID = newRole
						});
				}
				Context.SaveChanges();

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}

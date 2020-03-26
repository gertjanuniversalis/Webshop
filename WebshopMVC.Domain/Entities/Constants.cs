using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopMVC.Domain.Entities
{
	public static class Constants
	{
		public enum EUserRoles
		{
			Default,
			Admin,
			Support,
			Moderator
		}

		public static List<EUserRoles> CanUploadProduct => new List<EUserRoles> { EUserRoles.Admin, EUserRoles.Moderator };
		public static List<EUserRoles> CanEditProduct => new List<EUserRoles> { EUserRoles.Admin, EUserRoles.Moderator };
		public static List<EUserRoles> CanModifyUser=> new List<EUserRoles> { EUserRoles.Admin, EUserRoles.Moderator, EUserRoles.Support };
		public static List<EUserRoles> CanDeleteProduct => new List<EUserRoles> { EUserRoles.Admin };


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopMVC.Domain.Entities
{
	public class UserRole
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int RoleID { get; set; }
	}
}

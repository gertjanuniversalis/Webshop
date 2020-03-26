using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopMVC.Domain.Entities
{
	class ItemRating
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int ProductID { get; set; }
		public int Rating { get; set; }
	}
}

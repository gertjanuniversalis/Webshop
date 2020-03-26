using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopMVC.Domain.Entities
{
	public class Order
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public DateTime OrderTime { get; set; }
	}
}

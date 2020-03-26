using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopMVC.Domain.Utilities
{
	public class DateFunctions
	{
		public string Today()
		{
			return DateTime.Now.ToString();
		}

		public string TodayMinus(int days)
		{
			DateTime now = DateTime.Now;
			DateTime pastTime = now.AddDays(-1 * days);

			return pastTime.ToString();
		}
	}
}

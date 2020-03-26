using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopMVC.Domain.Entities;

namespace WebshopMVC.WebUI.Models
{
	public class CartViewModel
	{
		public Dictionary<ProductViewModel, int> CartItems { get; set; }
	}
}
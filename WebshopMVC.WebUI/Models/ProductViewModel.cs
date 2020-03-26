using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebshopMVC.Domain.Entities;

namespace WebshopMVC.WebUI.Models
{
	public class ProductViewModel
	{
		private Product Product;

		public int ID
		{
			get => Product.ID;
		}
		public string Name
		{
			get => Product.Name;
			set { Product.Name = value; }
		}
		public string Description
		{
			get => Product.Description;
			set { Product.Description = value; }
		}
		public string Summary
		{
			get => Product.Summary;
			set { Product.Summary = value; }
		}
		public decimal Price
		{
			get => Product.Price;
			set { Product.Price = value; }
		}

		public ProductViewModel(Product product)
		{
			this.Product = product;
		}
	}
}
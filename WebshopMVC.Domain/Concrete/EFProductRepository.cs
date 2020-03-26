using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Concrete
{
	public class EFProductRepository : IProductRepository
	{
		private EFDbContext Context = new EFDbContext();
		public int CreateProduct(Product product)
		{
			if (product != null)
			{
				Context.Products.Add(product);
				Context.SaveChanges();

				Product dbEntry = Context.Products.Find(product.ID);
				return dbEntry == null ? -1 : dbEntry.ID;
			}
			return -1;
		}

		public bool DeleteProduct(Product product)
		{
			return DeleteProduct(product.ID);
		}

		public bool DeleteProduct(int productID)
		{
			Product dbEntry = Context.Products.Find(productID);

			if (dbEntry == null)
			{
				Context.Products.Remove(dbEntry);
				Context.SaveChanges();
				return true;
			}
			return false;
		}

		

		public bool ModifyProduct(Product product)
		{
			if (product.ID != 0)
			{
				Product dbEntry = Context.Products.Find(product.ID);
				if (dbEntry != null)
				{
					dbEntry.Name = product.Name;
					dbEntry.Description = product.Description;
					dbEntry.Price = product.Price;
					//dbEntry.ImageData = product.ImageData;
					//dbEntry.ImageMimeType = product.ImageMimeType;

					Context.SaveChanges();
					return true;
				}
			}
			return false;
		}

		public double GetRating(int productID)
		{
			List<ItemRating> itemRatings = Context.ItemRatings.Where(ir => ir.ProductID == productID).ToList();

			if (itemRatings.Count > 0)
			{
				List<int> rankings = itemRatings.Select(r => r.Rating).ToList();

				double votes = rankings.Count();
				int totalRating = rankings.Sum();
				double rating = totalRating / votes;

				return rating;
			}
			else
			{
				return -1;
			}
		}

		public bool SetRating(int userID, int productID, int rating)
		{
			try
			{
				ItemRating itemRating = Context.ItemRatings.Where(r => r.UserID == userID && r.ProductID == productID).FirstOrDefault();

				if (itemRating == null)
				{
					Context.ItemRatings.Add(
						new ItemRating
						{
							UserID = userID,
							ProductID = productID,
							Rating = rating
						});
					Context.SaveChanges();
					return true;
				}
				else
				{
					itemRating.Rating = rating;
					Context.SaveChanges();
					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		public List<Product> GetAllProducts()
		{
			return Context.Products.Where(p => p.ID > 0).ToList();
		}

		public Product GetProductByID(int itemID)
		{
			return Context.Products.Find(itemID);
		}
	}
}

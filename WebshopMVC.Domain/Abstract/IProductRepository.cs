using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Abstract
{
	public interface IProductRepository
	{
		/// <summary>
		/// Creates a new product in the database, returning the productID
		/// </summary>
		int CreateProduct(Product product);

		List<Product> GetAllProducts();

		/// <summary>
		/// Attempts to remove a product from the database, returning the success
		/// </summary>
		bool DeleteProduct(Product product);

		Product GetProductByID(int itemID);

		/// <summary>
		/// Attempts to remove a product from the database, returning the success
		/// </summary>
		bool DeleteProduct(int productID);
		/// <summary>
		/// Updates a product to the new state, returning the success
		/// </summary>
		bool ModifyProduct(Product product);


		/// <summary>
		/// Sets or changes the user-specific rating (out of 5) for a product
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="productID"></param>
		/// <param name="rating"></param>
		/// <returns></returns>
		bool SetRating(int userID, int productID, int rating);

		/// <summary>
		/// Gets the average rating for this product
		/// </summary>
		/// <param name="productID"></param>
		/// <returns></returns>
		double GetRating(int productID);

	}
}

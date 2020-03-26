using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Abstract
{
	public interface IOrderRepository
	{
		/// <summary>
		/// Creates a new order for the supplied user; returning the OrderID
		/// </summary>
		int CreateOrder(int userID);
		/// <summary>
		/// Adds a list of products to the supplied Order
		/// </summary>
		bool AddProductToOrder(int orderID, IEnumerable<CartItem> cart);
		/// <summary>
		/// Removes a list of products from the supplied Order
		/// </summary>
		bool RemoveProductFromOrder(int orderID, IEnumerable<CartItem> cart);
		/// <summary>
		/// Removes an order from the database
		/// </summary>
		bool DeleteOrder(int orderID);
		/// <summary>
		/// Retrieves a shoppingcart from the database
		/// </summary>
		Cart GetOrder(int orderID);

		/// <summary>
		/// Gets the amount ordered from a specific product
		/// </summary>
		int GetAmountOrdered(Product product, int daysAgo = 0, DateTime? lastDate = null);
		/// <summary>
		/// Gets the amount ordered from a specific product
		/// </summary>
		int GetAmountOrdered(int productID, int daysAgo = 0, DateTime? lastDate = null);

		/// <summary>
		/// Gets the top X selling items during a period
		/// </summary>
		/// <param name="amountOfProducts">The amount of products to return</param>
		/// <param name="amountOfDays">The amount of days to check</param>
		/// <returns></returns>
		List<CartItem> GetTopSellers(int amountOfProducts, int amountOfDays);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Concrete
{
	public class EFOrderRepository : IOrderRepository
	{
		EFDbContext Context = new EFDbContext();

		public bool AddProductToOrder(int orderID, IEnumerable<CartItem> cart)
		{
			try
			{
				foreach (CartItem item in cart)
				{
					OrderDetail dbEntry = Context.OrderDetails.Where(op => op.OrderID == orderID && op.ProductID == item.Product.ID).FirstOrDefault();

					if (dbEntry == null)
					{
						Context.OrderDetails.Add(new OrderDetail { OrderID = orderID, ProductID = item.Product.ID, Quantity = item.Quantity });
					}
					else
					{
						dbEntry.Quantity += item.Quantity;
					}
				}
				Context.SaveChanges();
				return true;
			}
			catch
			{
				//TODO: add rollback
				return false;
			}
		}

		public int CreateOrder(int userID)
		{
			if (userID >= 0)
			{
				Order order = new Order { UserID = userID, OrderTime = DateTime.UtcNow };

				Context.Orders.Add(order);
				Context.SaveChanges();

				Order dbEntry = Context.Orders.Find(order.ID);
				return dbEntry == null ? -1 : dbEntry.ID;
			}
			return -1;
		}

		public bool DeleteOrder(int orderID)
		{
			Order dbEntry = Context.Orders.Find(orderID);
			if(dbEntry != null)
			{
				Context.Orders.Remove(dbEntry);
				Context.SaveChanges();
				return true;
			}
			return false;
		}

		public Cart GetOrder(int orderID)
		{
			var orderedProducts = Context.OrderDetails.Where(op => op.OrderID == orderID).ToList<OrderDetail>();

			if (orderedProducts?.Count() != 0)
			{
				Cart cart = new Cart();

				foreach (OrderDetail orderedProduct in orderedProducts)
				{
					cart.AddItem(Context.Products.Find(orderedProduct.ProductID), orderedProduct.Quantity);
				}

				return cart; 
			}
			else
			{
				return null;
			}
		}

		public bool RemoveProductFromOrder(int orderID, IEnumerable<CartItem> cart)
		{
			try
			{
				foreach (CartItem item in cart)
				{
					OrderDetail dbEntry = Context.OrderDetails.Where(op => op.OrderID == orderID && op.ProductID == item.Product.ID).FirstOrDefault();

					if (dbEntry == null)
					{
						Context.OrderDetails.Add(new OrderDetail { OrderID = orderID, ProductID = item.Product.ID, Quantity = item.Quantity });
					}
					else
					{
						dbEntry.Quantity -= item.Quantity;

						if (dbEntry.Quantity <= 0)
						{ Context.OrderDetails.Remove(dbEntry); }
					}
				}
				Context.SaveChanges();
				return true;
			}
			catch
			{
				//TODO: add rollback
				return false;
			}
		}

		public int GetAmountOrdered(Product product, int daysAgo = 0, DateTime? lastDate = null)
		{
			return GetAmountOrdered(product.ID, daysAgo, lastDate);
		}

		public int GetAmountOrdered(int productID, int daysAgo = 0, DateTime? lastDate = null)
		{
			int total = (from od in Context.OrderDetails where od.ProductID == productID select od.Quantity).Sum();
			return total;
		}


		public List<CartItem> GetTopSellers(int amountOfProducts, int amountOfDays)
		{
			DateTime now = DateTime.UtcNow;
			DateTime timeStart = now.AddDays(-amountOfDays);

			var groupedItemsInTime = (from o in Context.Orders
									  join od in Context.OrderDetails on o.ID equals od.OrderID
									  join p in Context.Products on od.ProductID equals p.ID
									  where o.OrderTime > timeStart
									  select new
									  {
										  p.ID,
										  p.Name,
										  p.Price,
										  p.Description,
										  p.Summary,
										  p.Image,
										  od.Quantity
									  }).GroupBy(pd => pd.ID);

			List<CartItem> topCart = new List<CartItem>();

			foreach (var itemGroup in groupedItemsInTime)
			{
				topCart.Add(new CartItem
					{
						Product = new Product
						{
							ID = itemGroup.Select(ig => ig.ID).FirstOrDefault(),
							Name = itemGroup.Select(ig => ig.Name).FirstOrDefault(),
							Price = itemGroup.Select(ig => ig.Price).FirstOrDefault(),
							Summary = itemGroup.Select(ig => ig.Summary).FirstOrDefault(),
							Description = itemGroup.Select(ig => ig.Description).FirstOrDefault(),
							Image = itemGroup.Select(ig => ig.Image).FirstOrDefault()
						},
						Quantity = itemGroup.Sum(ig => ig.Quantity)
					}
				);
			}

			var topXItems = topCart.OrderByDescending(tc => tc.Quantity).Take(amountOfProducts).ToList();

			return topXItems;
		}
	}
}

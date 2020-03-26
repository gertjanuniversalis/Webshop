using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopMVC.Domain.Concrete;
using WebshopMVC.Domain.Entities;

namespace WebshopMVC.Domain.Utilities
{
	[Obsolete]
	public class TopSellerAttempts
	{
		private EFDbContext Context = new EFDbContext();

		public List<CartItem> GetTopSellers1(int amountOfProducts, int amountOfDays)
		{
			DateTime now = DateTime.UtcNow;
			DateTime periodStart = now.Subtract(TimeSpan.FromDays(amountOfDays));

			var ordersInFrame = Context.Orders.Where(ord => ord.OrderTime > periodStart).ToList();
			if (ordersInFrame.Count() == 0)
			{ return null; }

			Cart cart = new Cart();

			foreach (Order order in ordersInFrame)
			{
				var orderItems = Context.OrderDetails.Where(op => op.OrderID == order.ID).ToList();

				foreach (OrderDetail detail in orderItems)
				{
					cart.AddItem(Context.Products.Find(detail.ProductID), detail.Quantity);
					//cart.AddItem(Context.Products.Where(prod => prod.ID == detail.ProductID).FirstOrDefault(), detail.Quantity);
				}
			}

			IEnumerable<CartItem> sortedCart = cart.Items.OrderBy(ci => ci.Quantity).Reverse();

			IEnumerable<CartItem> selection = sortedCart.Take(amountOfProducts);

			//List<CartItem> topItems = cart.Items.OrderBy(ci => ci.Quantity).Take(amountOfProducts).ToList();

			return selection.ToList();
		}

		public List<CartItem> GetTopSellers2(int amountOfProducts, int amountOfDays)
		{
			DateTime now = DateTime.UtcNow;
			DateTime timeStart = now.AddDays(-amountOfDays);

			var orders = (from detail in Context.OrderDetails
						  join order in Context.Orders on detail.OrderID equals order.ID
						  join product in Context.Products on detail.ProductID equals product.ID
						  where (order.OrderTime < now && order.OrderTime > timeStart)
						  select new
						  {
							  product.ID,
							  product.Name,
							  product.Price,
							  product.Description,
							  product.Summary,
							  detail.Quantity
						  }
						).GroupBy(entry => entry.ID).GroupBy(i => i.Key).ToList();

			var products = (from product in Context.Products
							join orderdetail in Context.OrderDetails on product.ID equals orderdetail.ProductID
							join order in Context.Orders on orderdetail.OrderID equals order.ID
							where order.OrderTime > timeStart
							select new
							{
								product.ID,
								product.Name,
								product.Price,
								product.Summary,
								product.Description,
								orderdetail.Quantity
							}).ToList();


			return new List<CartItem>();
		}


		public List<CartItem> GetTopSellers3(int amountOfProducts, int amountOfDays)
		{
			DateTime now = DateTime.UtcNow;
			DateTime timeStart = now.AddDays(-amountOfDays);

			var groupedItemsInTime = (from p in Context.Products
									  join od in Context.OrderDetails on p.ID equals od.ProductID
									  join o in Context.Orders on od.OrderID equals o.ID
									  where o.OrderTime > timeStart
									  select new
									  {
										  p.ID,
										  p.Name,
										  p.Price,
										  p.Description,
										  p.Summary,
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
						Price = itemGroup.Select(ig=>ig.Price).FirstOrDefault(),
						Summary = itemGroup.Select(ig=>ig.Summary).FirstOrDefault(),
						Description = itemGroup.Select(ig=>ig.Description).FirstOrDefault()
					},
					Quantity = itemGroup.Sum(ig => ig.Quantity)
				}
				);
			}

			var topXItems = topCart.OrderByDescending(tc => tc.Quantity).Take(amountOfProducts).ToList();

			return topXItems;
		}

		public List<CartItem> GetTopSellers4(int amountOfProducts, int amountOfDays)
		{
			DateTime now = DateTime.UtcNow;
			DateTime timeStart = now.AddDays(-amountOfDays);

			var groupedItemsInTime = (from p in Context.Products
									  join od in Context.OrderDetails on p.ID equals od.ProductID
									  join o in Context.Orders on od.OrderID equals o.ID
									  where o.OrderTime > timeStart
									  select new
									  {
										  Product = new
										  {
											  ID = p.ID,
											  Name = p.Name,
											  Price = p.Price,
											  Description = p.Description,
											  Summary = p.Summary,
										  },
										  Quantity = od.Quantity
									  }).GroupBy(pd => pd.Product.ID);


			List<CartItem> items = (List<CartItem>)groupedItemsInTime;

			return null;
		}
	}
}

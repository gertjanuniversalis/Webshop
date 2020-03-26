using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;
using WebshopMVC.Domain.Infrastructure.Abstract;
using WebshopMVC.WebUI.Infrastructure.Managers;
using WebshopMVC.WebUI.Models;

namespace WebshopMVC.WebUI.Controllers
{
    public class StoreController : Controller
    {
		private IProductRepository ProductRepository;
		private IOrderRepository OrderRepository;
		private IUserRepository UserRepository;
		private ISessionManager SessionManager;

		private IAuthorisation Authorisation;

		public StoreController(IProductRepository prodRepo, IOrderRepository orderRepo, 
			ISessionManager sessionManager, IUserRepository userRepo,
			IAuthorisation authorizer)
		{
			this.ProductRepository = prodRepo;
			this.OrderRepository = orderRepo;
			this.UserRepository = userRepo;
			this.SessionManager = sessionManager;

			this.Authorisation = authorizer;
		}

		public ViewResult Store()
		{
			List<Product> inventory = ProductRepository.GetAllProducts();

			ViewBag.LoggedIn = SessionManager.IsLoggedIn;

			return View(inventory);
		}

		public ActionResult ItemDetail(int itemID)
		{
			return ProductDetail(itemID);
		}

		public ActionResult ProductDetail(int itemID, bool horizontal = false, bool showDesc = false)
		{
			Product product = ProductRepository.GetProductByID(itemID);

			return ProductDetail(product, horizontal, showDesc);
		}

		public ViewResult ProductDetail(Product product, bool horizontal = false, bool showDesc = false)
		{
			ViewBag.Horizontal = horizontal;
			ViewBag.Fullscreen = horizontal;
			ViewBag.showDescription = showDesc;
			ViewBag.LoggedIn = SessionManager.IsLoggedIn;

			return View("ProductDetail", product);
		}

		public ViewResult Cart()
		{
			return View(SessionManager.Cart);
		}

		[HttpPost]
		public ViewResult FinalisePurchase()
		{
			int orderID = OrderRepository.CreateOrder(SessionManager.UserID);
			try
			{

				if (orderID > 0)
				{
					OrderRepository.AddProductToOrder(orderID, SessionManager.Cart.Items);
					
					return View("Thanks");
				}
				else
				{
					return View("Error");
				}
			}
			catch (Exception)
			{
				OrderRepository.DeleteOrder(orderID);
				return View("Error");
			}
		}

		public ViewResult TopSellers(int productsToShow = 5, int daysToCheck = 7)
		{
			var topItems = OrderRepository.GetTopSellers(productsToShow, daysToCheck);

			ViewBag.Days = daysToCheck;
			ViewBag.LoggedIn = SessionManager.IsLoggedIn;

			return View(topItems);
		}

		[HttpPost]
		public RedirectToRouteResult AddToCart(int ID, int Amount = 0, string returnUrl = "")
		{
			if (Amount > 0)
			{
				Product product = ProductRepository.GetProductByID(ID);
				if (product != null)
				{
					SessionManager.Cart.AddItem(product, Amount);
				}
			}
			return RedirectToAction("Store", new { returnUrl });
		}

		[HttpPost]
		public RedirectToRouteResult EmptyCart()
		{
			SessionManager.Cart.Empty();
			return RedirectToAction("Cart", "Store");
		}

		[HttpGet]
		public ViewResult EditItem(int itemID = -1)
		{
			Product product = ProductRepository.GetProductByID(itemID);

			if(product != null)
			{
				return View(product);
			}
			else
			{
				return View("Error");
			}
		}
		
		[HttpPost]
		public ActionResult EditItem (Product product)
		{
			if(Authorisation.CanEditProduct(SessionManager.UserToken))
			{
				if(ModelState.IsValid)
				{
					bool editSuccess = ProductRepository.ModifyProduct(product);

					if (editSuccess)
					{
						return ItemDetail(product.ID);
					}
					else
					{
						return View("Error");
					}
				}
				else
				{
					return View("EditItem", product);
				}
			}
			return View("Error");
		}
    }
}
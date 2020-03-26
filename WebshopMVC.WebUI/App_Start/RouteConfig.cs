using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebshopMVC.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			AddCustomRoutes(routes);

			//Keep this route last
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

		}

		private static void AddCustomRoutes(RouteCollection routes)
		{
			AddStoreRoutes(routes);
			AddFormRoutes(routes);
			AddUserRoutes(routes);
			AddAdminRoutes(routes);
		}

		/// <summary>
		/// Adds custom routings to communication forms
		/// </summary>
		private static void AddFormRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				"Contact",
				"Contact",
				new { controller = "Home", action = "Contact" }
			);
		}

		/// <summary>
		/// Adds routes related to user accounts
		/// </summary>
		private static void AddUserRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				"Login",
				"Login",
				new { controller = "User", action = "Login" }
			);
			routes.MapRoute(
				"Logout",
				"Logout",
				new { controller = "User", action = "Logout" }
			);
			routes.MapRoute(
				"Signup",
				"Signup",
				new { controller = "User", action = "Signup" }
			);
		}

		/// <summary>
		/// Adds routes related to webstore interactions
		/// </summary>
		/// <param name="routes"></param>
		private static void AddStoreRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				"Store",
				"Store",
				new { controller = "Store", action = "Store" }
			);
			routes.MapRoute(
				"ItemDetail",
				"Store/{itemID}",
				new { controller = "Store", action = "ItemDetail" },
				new { itemID = @"\d+" }
			);

			routes.MapRoute(
				"TopFive",
				"TopSellers",
				new { controller = "Store", action = "TopSellers" }
			);

			routes.MapRoute(
				"Cart",
				"Cart",
				new { controller = "Store", action = "Cart" }
			);
		}

		private static void AddAdminRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				"EditItem",
				"EditProduct/{itemID}",
				new { controller = "Store", action = "EditItem" },
				new { itemID = @"\d+" }
			);
		}
	}
}

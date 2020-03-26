using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopMVC.WebUI.Infrastructure.Managers;

namespace WebshopMVC.WebUI.Infrastructure.Concrete
{
    public class MenuLinks
    {
		public IEnumerable<LinkItem> LinkItems => SetLinks();

		private ISessionManager SessionManager;

        public MenuLinks(ISessionManager sessionManager)
        {
            this.SessionManager = sessionManager;
        }

        private IEnumerable<LinkItem> SetLinks()
        {
			bool loggedIn = SessionManager.IsLoggedIn;

			var links = new List<LinkItem>()
			{
				new LinkItem ( linkName: "Home", controller: "Home", action: "Home" ),
				new LinkItem ( linkName:"About", controller: "Home", action: "About"),
				new LinkItem ( linkName: "Contact", controller: "Home", action: "Contact"),
				new LinkItem ( linkName: "Store", controller: "Store", action: "Store" ),
				new LinkItem ( linkName: "TopSellers", controller: "Store", action: "TopSellers", pageName: "Top Five"),
				new LinkItem ( linkName: "Cart", controller: "Store", action: "Cart" ),
			};

			if (loggedIn)
			{
				links.Add(new LinkItem(linkName: "Profile", controller: "User", action: "UserPanel"));
				links.Add(new LinkItem(linkName: "Logout (" + SessionManager.UserName + ")", controller: "User", action: "Logout"));
            }
            else
            {
				links.Add(new LinkItem(linkName: "Login", controller: "User", action: "Login"));
				links.Add(new LinkItem(linkName: "Sign up", controller: "User", action: "Signup"));
            }

            return links;
        }
    }

    public class LinkItem
    {
        public string LinkName { get; }
        public string Controller { get; }
        public string Action { get; }
		public string PageName { get; }

		public LinkItem(string linkName, string controller, string action, string pageName = "")
		{
			LinkName = linkName;
			Controller = controller;
			Action = action;
			PageName = pageName == "" ? linkName : pageName;
		}
    }
}
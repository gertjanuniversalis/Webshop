using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WebshopMVC.Domain.Entities;
using WebshopMVC.WebUI.Models;

namespace WebshopMVC.WebUI.Infrastructure.Managers
{
	public class SessionManager : ISessionManager
	{
		public bool IsLoggedIn => UserToken != null;
		public string UserName => IsLoggedIn ? UserToken.Name : "";
		public string Email => IsLoggedIn ? UserToken.Email : "";
		public int UserID => IsLoggedIn ? UserToken.ID : -1;
		
		public Cart Cart {
			get { return (Cart)session[UserCartName] ?? new Cart(); }
			private set { session[UserCartName] = value; }
		}
		public User UserToken => (User)session[UserSessionName];



		private HttpContext context;
		private HttpSessionState session;

		private string UserSessionName = "User";
		private string UserCartName = "ShoppingCart";

		public SessionManager()
		{
			context = HttpContext.Current;
			session = context.Session;

			Cart.CartContentChanged += this.UpdateCart;

		}
		public void Login(User userToken)
		{
			session[UserSessionName] = userToken;
		}

		public void Logout()
		{
			session.Remove(UserSessionName);
		}

		private void UpdateCart(object s, CartChangedArgs cca)
		{
			this.Cart = cca.Cart;
		}
	}
}
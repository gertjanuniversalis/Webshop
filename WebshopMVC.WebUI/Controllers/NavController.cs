using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;
using WebshopMVC.Domain.Infrastructure.Abstract;
using WebshopMVC.WebUI.Infrastructure.Managers;

namespace WebshopMVC.WebUI.Controllers
{
    public class NavController : Controller
    {
		private ISessionManager SessionManager;
		private IAuthorisation Authorisation;
		private IUserRepository UserRepository;

        public NavController(ISessionManager sessionManager, IAuthorisation authorisation, IUserRepository userRepository)
        {
			this.SessionManager = sessionManager;
			this.Authorisation = authorisation;
			this.UserRepository = userRepository;
        }

        public PartialViewResult Menu(bool horizontalLayout = false)
        {
			string url = Request.RawUrl;

			string[] urlChunk = url?.Split(new char[] { '/' });
			string pageName = (urlChunk.Last() == "") ? "Home" : urlChunk.Last();

			ViewBag.ActivePage = pageName;
            return PartialView(new Infrastructure.Concrete.MenuLinks(SessionManager).LinkItems);
        }

		public PartialViewResult UploadButton()
		{
			if (!SessionManager.IsLoggedIn)
			{ return null; }

			if (Authorisation.CanUploadProduct(SessionManager.UserToken))
			{
				return PartialView("_UploadButton");
			}
			else
			{
				return null;
			}
		}

		public PartialViewResult EditButton(int itemID)
		{
			if(!SessionManager.IsLoggedIn)
			{ return null; }

			if (Authorisation.CanEditProduct(SessionManager.UserToken))
			{
				return PartialView("_EditButton", itemID);
			}
			else
			{
				return null;
			}
		}
    }
}
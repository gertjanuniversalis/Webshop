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
    public class AdminController : Controller
    {
		private IUserRepository UserRepository;
		private ISessionManager SessionManager;
		private IAuthorisation Authorisation;

		public AdminController(IUserRepository userRepo, ISessionManager session, IAuthorisation authorisation)
		{
			this.UserRepository = userRepo;
			this.SessionManager = session;
			this.Authorisation = authorisation;
		}



        public ActionResult Index()
        {
			return Redirect("Index");
        }

		[HttpGet]
		public ViewResult EditUser(int shownUserID)
		{
			if (Authorisation.CanModifyUser(shownUserID, SessionManager.UserToken))
			{
				User user = UserRepository.GetUserByID(shownUserID);

				if (user != null)
				{
					UserViewModel userModel = new UserViewModel(user);
					return View(userModel);
				}
				else
				{
					return View("Error");
				}
			}
			else
			{
				return View("Error");
			}
		}

		[HttpPost]
		public ActionResult EditUser(UserViewModel userModel)
		{
			if (ModelState.IsValid)
			{
				return View("Thanks");
			}
			else
			{
				return View(userModel);
			}
		}
    }
}
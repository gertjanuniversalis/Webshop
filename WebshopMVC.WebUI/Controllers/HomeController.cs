using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopMVC.Domain.Abstract;
using WebshopMVC.WebUI.Infrastructure.Managers;

namespace WebshopMVC.WebUI.Controllers
{
    public class HomeController : Controller
    {
		private ISessionManager SessionManager;

		public HomeController(ISessionManager sessionManager)
		{
			SessionManager = sessionManager;
		}

		// GET: Home
		public ActionResult Index()
        {
            return View("Home");
        }

		public ActionResult Home()
		{
			return View();
		}

        public ActionResult About()
        {
            return View();
        }

		[HttpGet]
		public ActionResult Contact()
		{
			var model = new Models.ContactFormModel();

			if (SessionManager.IsLoggedIn)
			{
				model.Email = SessionManager.Email;
				model.UserName = SessionManager.UserName;
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult Contact(Models.ContactFormModel contactForm)
		{
			if(ModelState.IsValid)
			{
				return View("Thanks");
			}
			else
			{
				return View(contactForm);
			}
		}
    }
}
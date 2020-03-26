using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;
using WebshopMVC.WebUI.Infrastructure.Managers;

namespace WebshopMVC.WebUI.Controllers
{
    public class RatingController : Controller
    {
		private IProductRepository ProductRepository;
		private ISessionManager SessionManager;

		public RatingController(IProductRepository prodRepo, ISessionManager sessionMgr)
		{
			this.ProductRepository = prodRepo;
			this.SessionManager = sessionMgr;
		}

        public PartialViewResult RatingImage(int prodID)
		{
			double productRating = ProductRepository.GetRating(prodID);

			ViewBag.Rating = Math.Round(productRating, 2);
			ViewBag.ProdID = prodID;

			return PartialView("_RatingImage");
		}

		public ActionResult EditRating(int prodID, int rating, string returnUrl)
		{
			if (SessionManager.IsLoggedIn)
			{
				bool success = ProductRepository.SetRating(SessionManager.UserID, prodID, rating);
			}

			return Redirect(returnUrl);
		}
	}
}
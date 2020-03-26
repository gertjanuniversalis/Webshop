using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Entities;
using WebshopMVC.Domain.Infrastructure.Abstract;
using WebshopMVC.WebUI.Infrastructure.Abstract;
using WebshopMVC.WebUI.Infrastructure.Managers;
using WebshopMVC.WebUI.Models;

namespace WebshopMVC.WebUI.Controllers
{
    public class UserController : Controller
    {
		private ISessionManager SessionManager;
		private IUserAuthenticator Authenticator;
		private IUserRepository UserRepository;
		private IAuthorisation Authorisation;

		public UserController(ISessionManager sessionManager, IUserAuthenticator authenticator,
			IUserRepository userRepository, IAuthorisation authoriser)
		{
			this.UserRepository = userRepository;
			this.SessionManager = sessionManager;
			this.Authenticator = authenticator;
			this.Authorisation = authoriser;
		}

		[HttpGet]
        public ViewResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returlUrl)
		{
			//TODO: Pull the user from the DB only once
			if (ModelState.IsValid)
			{
				User userToken = Authenticator.CanLogin(model);
				if(userToken != null)
				{
					SessionManager.Login(userToken);
					return Redirect(returlUrl ?? Url.Action("About", "Home"));
				}
				else
				{
					ViewBag.Incorrect = true;
					return View();
				}
			}
			else
			{
				return View();
			}
		}

		public RedirectToRouteResult Logout()
		{
			SessionManager.Logout();

			return RedirectToAction("Home", "Home");
		}

		[HttpGet]
		public ActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SignUp(SignUpViewModel signUpModel)
		{
			if (ModelState.IsValid)
			{
				User existingMail = UserRepository.GetUserByEmail(signUpModel.Email);

				if (existingMail == null)
				{
					User user = new User
					{
						Name = signUpModel.UserName,
						Email = signUpModel.Email,
						Password = signUpModel.PassWord
					};

					int UserID = UserRepository.CreateUser(user);

					if (UserID > 0)
					{
						return Redirect(Url.Action("About", "Home"));
					}
					else
					{
						ModelState.AddModelError("General", "Something went wrong: please resubmit");
						return View(signUpModel);
					}
				}
				else
				{
					ModelState.AddModelError("Email", "Email already registered");
					return View(signUpModel);
				}
			}
			else
			{
				return View(signUpModel);
			}
		}

		[HttpGet]
		public ActionResult UserPanel(int userID = -1)
		{
			int requestedUser = userID > 0 ? userID : SessionManager.UserID;


			if (Authorisation.CanModifyUser(requestedUser, SessionManager.UserToken))
			{
				User user = UserRepository.GetUserByID(requestedUser);
				return View(user); 
			}
			else
			{
				return View("Error");
			}
		}

		[HttpPost]
		public ActionResult UserPanel(User user)
		{
			if (Authorisation.CanModifyUser(user.ID, SessionManager.UserToken))
			{
				if(ModelState.IsValid)
				{
					bool editsuccess = UserRepository.ModifyUser(user);

					if (editsuccess)
					{
						ViewBag.Comment = "Changes Applied";
						return View("UserPanel", user);
					}
					else
					{
						return View("Error");
					}
				}
				else
				{
					return View("UserPanel", user);
				}
			}
			return View("Error");
		}
    }
}
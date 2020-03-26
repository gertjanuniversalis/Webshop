using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebshopMVC.Domain.Entities;
using WebshopMVC.WebUI.Models;

namespace WebshopMVC.WebUI.Infrastructure.Managers
{
    public interface ISessionManager
    {
        bool IsLoggedIn { get; }
        string UserName { get; }
        string Email { get; }
		int UserID { get; }

		User UserToken { get; }
		Cart Cart { get; }

        void Login(User userToken);

        void Logout();
    }
}

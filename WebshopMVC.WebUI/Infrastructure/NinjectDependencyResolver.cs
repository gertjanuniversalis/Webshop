using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ninject;

using WebshopMVC.Domain.Abstract;
using WebshopMVC.Domain.Concrete;
using WebshopMVC.Domain.Infrastructure.Abstract;
using WebshopMVC.Domain.Infrastructure.Concrete;
using WebshopMVC.WebUI.Infrastructure.Abstract;
using WebshopMVC.WebUI.Infrastructure.Concrete;
using WebshopMVC.WebUI.Infrastructure.Managers;

namespace WebshopMVC.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
			//SessionData
			kernel.Bind<ISessionManager>().To<SessionManager>().InSingletonScope();

			//Authentification
			kernel.Bind<IUserAuthenticator>().To<UserAuthenticator>();

			//Authorisation
			kernel.Bind<IAuthorisation>().To<Authorisation>();

			//Database Access
			kernel.Bind<IUserRepository>().To<EFUserRepository>().InSingletonScope();
			kernel.Bind<IProductRepository>().To<EFProductRepository>().InSingletonScope();
			kernel.Bind<IOrderRepository>().To<EFOrderRepository>().InSingletonScope();
        }
    }
}
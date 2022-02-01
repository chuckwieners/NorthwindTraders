using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using NorthwindTraders.Domain.Repositories;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.MVC.App_Start;

namespace NorthwindTraders.MVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            //OUR CODE:
            ////There are 3 ways we could register the repositories
            ////1)register/boostrap/tie our interface to the ef project
            //container.RegisterType<ICategories, Categories>();
            ////for this to work, we have to 
            ////************add to our global.asax.appStart()*****************
            ////(per readme file)
            //container.RegisterType<IProducts, Products>();
            //container.RegisterType<ISuppliers, Suppliers>();
            ////Register all applicable interfaces and retrurned objects.

            //2 created the repositoryRegistrar
            //then  add this code
            RepositoryRegistrar.Config(container);

           

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
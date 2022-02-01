using Microsoft.Practices.Unity;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindTraders.MVC.App_Start
{
    /// <summary>
    /// This class is NOT needed and this code couls be done within the 
    /// unity.config file, the reason this file was created
    /// was to separate the repositories for registry/bootstrapping
    /// </summary>

    public class RepositoryRegistrar
    {
        public static void Config(IUnityContainer container)
        { 
            ////Manually register the repositories using reflection

            ////Scan the northwindTraders Domain (Project/assembly) for 
            ////all generic repository interfaces.
            ////1) Find the assembly where IProducts is located
            ////2) Get all types within that assembly where the type is an interface
            ////   and (if the Namespace is null, default to an empty string.) the
            ////   namespace ends with repositories

            //var genericTypes =
            //    typeof(IProducts).Assembly
            //    .GetTypes()
            //    //?? says if null change to ""
            //    .Where(t => t.IsInterface && (t.Namespace ?? "")
            //    .EndsWith("Repositories"));
            ////finds only stuff in the repositories folder.
            
            ////Scan Northwind traders.data.ef for all concrete repository
            ////implementations.
            ////1)Find the assembly where Products is located
            ////2) Get all types within the assembly where the type is a class
            ////  (if the namespace is null default to empty string) 
            ////   the namespace name ends with Repositories.
            //var concreteTypes = typeof(Products).Assembly
            //    .GetTypes()
            //    .Where(t => t.IsClass && (t.Namespace ?? "")
            //        .EndsWith("Repositories"));
            ////finds only the stuff in the repositories folder for the EF layer


            ////match up the appropriate (generic) interface types
            ////to the (concrete) class.

            //foreach (var pluginType in genericTypes)
            //{ 
            //    //remove the I from the interface name
            //    var concreteName = pluginType.Name.Remove(0, 1);
            //    //and find the matching concrete class
            //    var type = concreteTypes.FirstOrDefault(t => t.IsClass &&
            //        t.Name == concreteName);

            //    //make sure that a match was found
            //    if (type != null)
            //    { 
            //        //add the repository registration/boostrapping to the
            //        //container

            //        container.RegisterType(pluginType, type);
            //    }
                
            //}

            //3) to use the unity container to automatically register
            //ALL of the repositories
            //this code replaces the commented code above
            container.RegisterTypes(AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);
            //this is the same code as above, just using Unity to resolve
            //the interface and the concrete class.


        }

    }
}
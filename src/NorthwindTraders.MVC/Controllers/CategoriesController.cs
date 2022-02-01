using System;
using System.Linq;
using System.Web.Mvc;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;

namespace NorthwindTraders.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategories _categories;

        //DI Change - require an instance of ICategories

        public CategoriesController(ICategories categories)
        {
            //_categories = new Categories();
            //DI Change
            _categories = categories;
            //this is the start of implementing DI/IOC.  From here
            //use NuGet for Unity.mvc5.
            //Then go to the App Start Folder and view the Unity.Config.

        }

        // GET: Categories
        public ActionResult Index()
        {
            var categories = _categories.All.OrderBy(x => x.Name).ToList();

            //exception handling review
            //try catch is the mechanism used to handle exceptions within the code
            //for this example we will throw an instance of the base class for 
            //all exceptions which is Exception.

            //in the catch we will assign viewbag objects representing the different
            //messaging available (i.e. 4 standard properties of the exception class)

            try
            {
                Exception e = new Exception("This is MY Exception");
                //potentially dangerous code goes.
                
                //there is a property of the exception class that allows developers
                //to create custom messaging beyond just the message property,
                //that is the Hashtable for the Data Property

                e.Data.Add("1234", "This is chucks exception.");

                throw e;

            }
            catch (Exception ex)
            {
                //"Handling of the exception"
                //display the 4 properties of the exception
                ViewBag.Source = ex.Source;
                ViewBag.StackTrace = ex.StackTrace;
                ViewBag.Message = ex.Message;
                ViewBag.Data = ex.Data["1234"];

                //remove the existence of the exceptions from the server
                Server.GetLastError();
                Server.ClearError();
                //this should appear in most error handling code, you are probably
                //logging this somewhere using log4net or System.IO, you do not
                //need to continue the tracking on the server.





                //streamwriter is the class that allows us to write to a file
                //System.IO.StreamWriter sr = 
                //    new System.IO.StreamWriter("logger.txt", true);

                ////write & Writeline method
                //sr.WriteLine(DateTime.Now + " " + ex.StackTrace + " " +
                //    User.Identity.Name);

                ////make sure all data goes to the file
                //sr.Flush();
                ////closes the gateway to the file (performance measure)
                //sr.Close();

                
            }
            finally
            {
                //always executes
                ViewBag.Finally = "This code always executes.";
            }


            return View("index", categories);


        }

        public ActionResult Edit(int? id = 0)
        {
            if (id == 0)
            {
                //TODO: what do we do if the id parameter is null?
            }

            var category = _categories.FindBy(id.Value);
            return View("Edit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View("Edit", category);

            try
            {
                _categories.Update(category);
            }
            catch (Exception ex)
            {
                //TODO: is there other tasks needed to be done other than logging the exception?
            }

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View("Add", new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if (!ModelState.IsValid)
                return View("Add", category);

            try
            {
                _categories.Add(category);
            }
            catch (Exception ex)
            {
                //TODO: is there other tasks needed to be done other than logging the exception?
            }

            return RedirectToAction("Index");
        }
    }
}
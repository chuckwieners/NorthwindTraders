using System.Linq;
using System.Web.Mvc;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;

namespace NorthwindTraders.MVC.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISuppliers _suppliers;

        public SuppliersController()
        {
            _suppliers = new Suppliers();
        }

        public ActionResult Index()
        {
            var suppliers = _suppliers.All.OrderBy(x => x.Name);
            return View("index", suppliers);
        }

        public ActionResult Details(int? id = 0)
        {
            if (id == 0)
            {
                //TODO: what do we do if the id parameter is null? redirect to index?
            }

            var supplier = _suppliers.FindBy(id.Value);
            return View("Details", supplier);
        }

        public ActionResult Edit(int? id = 0)
        {
            if (id == 0)
            {
                //TODO: what do we do if the id parameter is null?
            }

            var supplier = _suppliers.FindBy(id.Value);
            return View("Edit", supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                //TODO: add notification message?
                return View("Edit", supplier);
            }

            //TODO: wrap repo call in try/catch .. log exception
            _suppliers.Update(supplier);
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View("Add", new Supplier());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                //TODO: add notification message?
                return View("Add", supplier);
            }

            //TODO: wrap repo call in try/catch .. log exception
            _suppliers.Add(supplier);
            return RedirectToAction("Index");
        }
    }
}
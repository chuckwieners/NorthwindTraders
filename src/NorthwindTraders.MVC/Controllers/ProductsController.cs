using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NorthwindTraders.Data.EF.Repositories;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Repositories;
using NorthwindTraders.Domain.Search.Criteria;
using NorthwindTraders.Domain.Search.Criteria.Results;
using NorthwindTraders.MVC.Enums;
using NorthwindTraders.MVC.Helpers;
using PagedList;

namespace NorthwindTraders.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts _products;
        private int _pageSize = 10;

        public ProductsController()
        {
            _products = new Products();
        }

        public ActionResult Index(ProductSearchCriteria criteria, ProductSortOrderEnum? sortOrder, int? page = 1)
        {
            if (criteria == null)
                //create default search criteria 
                criteria = new ProductSearchCriteria();

            var model = _products.Search(criteria);
            
            //NOTE: alternatives for sorting:
            // use Dynamic Linq library to pass the order by column/field as a string.
            // move the switch statement into a private method within this controller.
            // add a drop down list with the fields that can be sorted, and a 2nd ddl for sort direction
            List<ProductResult> results = null;
            switch (sortOrder)
            {
                case ProductSortOrderEnum.Name:
                    results = model.OrderBy(x => x.Name).ToList();
                    break;
                case ProductSortOrderEnum.UnitPrice:
                    results = model.OrderBy(x => x.Price).ToList();
                    break;
                case ProductSortOrderEnum.UnitsInStock:
                    results = model.OrderBy(x => x.UnitsInStock).ToList();
                    break;
                default:
                    results = model.ToList();
                    break;
            }
            
            //NOTE: this model can be passed with this method
            //or a new VM can be created that contains both a collection of results
            //and SearchCriteria properties
            ViewBag.SearchCriteria = criteria;
            ViewBag.SortOrder = sortOrder;
            //Get collections for Dropdown Lists.
            PrepDropDownLists();

            return View("Index", results.ToPagedList(page.Value, _pageSize));
        }

        public ActionResult Details(int? id = 0)
        {
            if (id == 0)
            {
                //TODO: what do we do if the id parameter is null? redirect to index?
            }

            var product = _products.FindBy(id.Value);
            return View("Details", product);
        }

        public ActionResult Edit(int? id = 0)
        {
            if (id == 0)
            {
                //TODO: what do we do if the id parameter is null? redirect to index?
            }

            PrepDropDownLists();
            var product = _products.FindBy(id.Value);
            return View("Edit", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                PrepDropDownLists();
                //TODO: add notification message?
                return View("Edit", product);
            }

            //TODO: wrap repo call in try/catch .. log exception
            _products.Update(product);
            //TODO: add notification message
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            PrepDropDownLists();
            return View("Add", new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                PrepDropDownLists();
                //TODO: add notification message?
                return View("Add", product);
            }

            //TODO: wrap repo call in try/catch .. log exception
            _products.Add(product);
            //TODO: add notification message
            return RedirectToAction("Index");
        }

        private void PrepDropDownLists()
        {
            //We can again use this method or create ChildActions in the 
            //domain related controllers that render the needed DDL's (which also limit reuse) or
            //probably the better way is to create Actions that return JSON
            //and use JS to call and populate the DDL's .. many would argue that 'newing' these
            //other repo's in this controller would be a code smell (mixing domain [Not being SRP])
            ViewBag.Categories = new Categories().All.ToSelectList(x => x.Name, x => x.Id);
            ViewBag.Suppliers = new Suppliers().All.ToSelectList(x => x.Name, x => x.Id);
        }
    }
}
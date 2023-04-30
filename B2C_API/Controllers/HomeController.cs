using B2C_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B2C_API.Models;

namespace B2C_API.Controllers
{
    public class HomeController : Controller
    {
        //PhoneManagerEntities _db = new PhoneManagerEntities();
        ProductsController productsController = new ProductsController();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View(productsController.GetProducts().ToList());

        }
    }

}

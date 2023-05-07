using B2C_API.Models;
using Fluent.Infrastructure.FluentModel;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace B2C_API.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<Products> products = new List<Products>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324/");
                HttpResponseMessage response = await client.GetAsync("api/Products");
                if (response.IsSuccessStatusCode)
                {
                    products = await response.Content.ReadAsAsync<List<Products>>();
                    return View(products);
                }
                else
                {
                    return RedirectToAction("Error", "error_404","Home");
                }
            }

        }
        public ActionResult error_404()
        {
            return View();
        }
    }

}

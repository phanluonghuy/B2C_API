using B2C_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace B2C_API.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        PhoneManagerEntities _db = new PhoneManagerEntities();
        public ActionResult Index()
        {
            return View();
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(string id)
        {
            var product = _db.Products.SingleOrDefault(s => s.ProductID == id);
            if (product != null)
            {
                GetCart().Add(product);
            }
            //return RedirectToAction("ShowToCart", "ShoppingCart");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Product(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_product = form["Product_ID"];
            int quantity_prodcut = int.Parse(form["Product_Quantity"]);
            cart.update_quantity(id_product, quantity_prodcut);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveProduct(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.remove_product(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.total_quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            //if (!ModelState.IsValid)
            //{
            //    // If the order data is not valid, return a bad request response with error details
            //    return BadRequest(ModelState);
            //}
            //var client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44324");
            //Cart cart = Session["Cart"] as Cart;
            //var _orders_Guest = new Orders_Guest
            //{
            //        Name = form["Name"],
            //        Phone = form["Phone"],
            //        Email = form["Email"],
            //        Address = form["Address"],
            //        OrderID = DateTime.Now.ToString("yyyyMMddHHmmss"),
            //        OrderDate = DateTime.Now,
            //        TotalAmount = decimal.Parse(form["Total_money"].ToString()),
            //        PaymentMethod = form["Payment"],
            //        Status = "Pending",
            //};
            //var response = await client.PostAsJsonAsync("api/Orders_Guest", _orders_Guest);
            //if (response.IsSuccessStatusCode)
            //{
            //    // The order was created successfully
            //    // Handle the response as needed
            //    return RedirectToAction("Checkout_Success", "ShoppingCart");
            //    //return "Succes";
            //}
            //else
            //{

            //    // The request failed
            //    // Handle the error as needed
            //    return RedirectToAction("Checkout_Fail", "ShoppingCart");
            //}
            var _orders_Guest = new Orders_Guest
            {
                Name = form["Name"],
                Phone = form["Phone"],
                Email = form["Email"],
                Address = form["Address"],
                OrderID = DateTime.Now.ToString("yyyyMMddHHmmss"),
                OrderDate = DateTime.Now,
                TotalAmount = decimal.Parse(form["Total_money"].ToString()),
                PaymentMethod = form["Payment"],
                Status = "Pending",
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324");
                var result = client.PostAsync("/api/Orders_Guest", _orders_Guest, new JsonMediaTypeFormatter()).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Checkout_Fail", "ShoppingCart");
                }
                else
                {
                    string content = result.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Checkout_Succes", "ShoppingCart");
                }
            }
        }

    }
}
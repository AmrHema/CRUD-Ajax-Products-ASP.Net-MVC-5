using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD_jQuery_Ajax.Models;

namespace CRUD_jQuery_Ajax.Controllers
{
    public class CategoriesController : Controller
    {
        private DB_Context db = new DB_Context();

        // GET: Categories
        public ActionResult Index()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Cat_id", "Cat_Name");
            return View();
        }

       

        public ActionResult GetProductsList(int Cat_id)
        {
            List<Products> products = db.Products.Where(x => x.Cat_id == Cat_id).ToList();

            ViewBag.products = new SelectList(products, "Product_id", "Product_Name");

            return PartialView("ProductPartial");

        }

        public ActionResult listCheck()
        {
            List<MyShop> ItemList = new List<MyShop>();
            ItemList.Add(new MyShop { ItemID = 1, ItemName = "Rice", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 2, ItemName = "Pulse", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 3, ItemName = "Chocholate", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 4, ItemName = "Soap", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 5, ItemName = "Tea", IsAvailable = true });

            ViewBag.ItemList = ItemList;

            return View();
        }


    }
}

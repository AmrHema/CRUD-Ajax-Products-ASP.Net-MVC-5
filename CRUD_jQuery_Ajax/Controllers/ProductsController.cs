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

    //[RoutePrefix("Products")]
    //[Route("{action=Index}")]

    public class ProductsController : Controller
    {
        private DB_Context db = new DB_Context();

        //[Route("All")]
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }

        public ActionResult PreviewProducts()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }
        // GET: Products JSON

        public JsonResult GetListProducts2()
        {
            var products = db.Products.ToList();

            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }
        // GET: searchProducts

        public ActionResult GetSearchRecord(string SearchText)
        {
            var products = db.Products.Include(p => p.Categories).Where(x => x.Product_Name.Contains(SearchText) || x.Product_Price.ToString().Contains(SearchText) || x.Product_QTY.ToString().Contains(SearchText));
            return PartialView("searchProds", products.ToList());
        }

       
        public ActionResult AutoComplete()
        {
            return View();
        }

        // Autocomplete
        [HttpGet]
        public JsonResult GetProducts(string text)
        {
            List<string> list = new List<string>();
            list = db.Products.Where(x => x.Product_Name.ToLower().Contains(text.ToLower())).Select(x => x.Product_Name).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //[Route("Top3")]

        // GET: Products
        public ActionResult Top3()
        {
            var products = db.Products.Include(p => p.Categories).OrderBy(x => x.Product_id).Take(3);
            return View(products.ToList());
        }


        // SortableImages
        public ActionResult SortableImages()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }

        //[Route("Details/{id}")]

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }



        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cat_id,Product_id,Product_Name,Product_Price,Product_QTY,Product_Writing_Date,Product_Description,Product_Image")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                var prods = db.Products.Include(p => p.Categories);
                return PartialView("PartialProductsTable", prods.ToList());
            }

            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name", products.Cat_id);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name", products.Cat_id);
            return View(products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: Products JSON
        public ActionResult Edit(Products pros)
        {
            if (pros.Product_id == 0)
            {
                db.Products.Add(pros);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    var products = db.Products.Include(p => p.Categories);
                    return PartialView("PartialProductsTable", products.ToList());

                }
                else
                    return Json(new { success = false, msg = "Failed Added" });
            }
            else
            {
                db.Entry(pros).State = System.Data.Entity.EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                {
                    var products = db.Products.Include(p => p.Categories);
                    return PartialView("PartialProductsTable", products.ToList());

                }
                else
                    return Json(new { success = false, msg = "Failed Update" });
            }

        }




        [HttpPost]
        public ActionResult DeleteProduct(int? Product_id)
        {
            if (Product_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(Product_id);
            if (products == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Products.Remove(products);
                db.SaveChanges();
            }
            return Json(new { success = true, d = "Deleted Succeded" }, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_jQuery_Ajax.Models;


namespace CRUD_jQuery_Ajax.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SideMenu()
        {
            List<MenuItem> list = new List<MenuItem>();
            list.Add(new MenuItem { Link = "/Home/Index", LinkName = "Home" });
            list.Add(new MenuItem { Link = "/Account/Login", LinkName = "Login" });
            list.Add(new MenuItem { Link = "/Account/Register", LinkName = "Register" });
            list.Add(new MenuItem { Link = "/Products/Index", LinkName = "Products" });
            return PartialView("SideMenu", list);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Controllers
{
    public class HomeController : Controller
    {   
        private readonly WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        public ActionResult DanhmucPHome()
        {
            var danhmuc = db.LoaiSanPhams.ToList();
            return PartialView("_DanhmucPHome", danhmuc);
        }
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
    }
}
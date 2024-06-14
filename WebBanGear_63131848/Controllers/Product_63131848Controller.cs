using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Controllers
{
    public class Product_63131848Controller : Controller
    {
        WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();
        // GET: Product_63131848

        //Tim Kiem big bar
        [HttpGet]
        public ActionResult TimKiem(string tenSP)
        {   
            var sanPhams = db.SanPhams.Where(sp => sp.TenSP.Contains(tenSP)).ToList();
            ViewBag.tenSP = tenSP;
            ViewBag.ProductCount = sanPhams.Count();
            
            if (sanPhams.Count == 0)
            {
                ViewBag.ProductCount = 0;
            }
                
            return View(sanPhams);
        }


        public ActionResult ProductFilter(string categoryName)
        {
            ViewBag.ProductCount = db.SanPhams.Where(x => x.LoaiSanPham.TenLoai == categoryName).Count();
            var items = db.SanPhams.Where(x => x.LoaiSanPham.TenLoai == categoryName).ToList();
            return View(items);
        }


        public ActionResult Index(string id)
        {
            var products = db.SanPhams.ToList();
            ViewBag.ProductCount = db.Database.SqlQuery<int>("SELECT COUNT(*) FROM SanPham").Single();
            var items = db.SanPhams.ToList();
            if (id != null)
            {
                items = items.Where(x => x.MaLoaiSP == id).ToList();
            }
            return View(products);
        }



        public ActionResult DanhmucPartial()
        {
            var danhmuc = db.LoaiSanPhams.ToList();
            return PartialView("_DanhmucPartial", danhmuc);
        }

        //San pham details
        public ActionResult Details(string id)
        {   
            var details = db.SanPhams.SingleOrDefault(n=>n.MaSP == id);
            if (details == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(details);
        }

    }
}
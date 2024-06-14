using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Controllers
{
    public class DonHang_63131848Controller : Controller
    {   
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();
        // GET: DonHang_63131848
        public ActionResult Index()
        {   
            if (Session["use"] == null)
            {
                return RedirectToAction("DangNhap","DKDN_63131848");
            }
            NguoiDung nd = (NguoiDung)Session["use"];
            string maND = nd.MaNguoiDung.ToString();
            var donhang = db.DonHangs.Where(x => x.MaNguoiDung == maND).ToList();
            return View(donhang.ToList());
        }

        public ActionResult Details(string maDH)
        {
            if (maDH == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DonHang dh = db.DonHangs.Find(maDH);
            var chitiet = db.ChiTietDonHangs.Where(d => d.MaDH == maDH).ToList();

            if (dh == null)
            {
                return HttpNotFound();
            }

            return View(chitiet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Areas.Admin.Controllers
{
    public class User_63131848Controller : Controller
    {
        
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();


        // GET: Admin/User_63131848
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.Include(n => n.PhanQuyen);
            return View(nguoiDungs.ToList());
        }
        [HttpGet]
        public ActionResult Index(string maND = "", string tenND = "")
        {   
            ViewBag.maND = maND;
            ViewBag.tenND = tenND;
            var nguoiDungs = db.NguoiDungs.SqlQuery("TimKiemND '" + maND + "', N'" + tenND + "'");
            if(nguoiDungs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(nguoiDungs.ToList());
        }

        string LayMaUser()
        {
            var maMax = db.NguoiDungs.ToList().Select(n => n.MaNguoiDung).Max();
            int maND = int.Parse(maMax.Substring(2)) + 1;
            string ND = String.Concat("00", maND.ToString());
            return "ND" + ND.Substring(maND.ToString().Length - 1);
        }
        // GET: Admin/User_63131848/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoiDung = LayMaUser();
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen");
            return View();
        }

        // POST: Admin/User_63131848/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNguoiDung,HoTenND,TenDangNhap,Email,DiaChiND,MatKhau,MaQuyen")] NguoiDung nguoiDung)
        {
            if (db.NguoiDungs.Any(nd => (nd.TenDangNhap == nguoiDung.TenDangNhap || nd.Email == nguoiDung.Email) && nd.MaNguoiDung != nguoiDung.MaNguoiDung))
            {
                ModelState.AddModelError("", "Tên tài khoản/Email đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                nguoiDung.MaNguoiDung = LayMaUser();
                if (string.IsNullOrEmpty(nguoiDung.HoTenND))
                    nguoiDung.HoTenND = "N/A";
                if (string.IsNullOrEmpty(nguoiDung.TenDangNhap))
                    nguoiDung.TenDangNhap = "N/A";
                if (string.IsNullOrEmpty(nguoiDung.Email))
                    nguoiDung.Email = "N/A";
                if (string.IsNullOrEmpty(nguoiDung.DiaChiND))
                    nguoiDung.DiaChiND = "N/A";
                if (string.IsNullOrEmpty(nguoiDung.MatKhau))
                    nguoiDung.MatKhau = "N/A";
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen", nguoiDung.MaQuyen);
            ViewBag.MaNguoiDung = nguoiDung.MaNguoiDung;
            return View(nguoiDung);
        }

        // GET: Admin/User_63131848/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen", nguoiDung.MaQuyen);
            return View(nguoiDung);
        }

        // POST: Admin/User_63131848/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,HoTenND,TenDangNhap,Email,DiaChiND,MatKhau,MaQuyen")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen", nguoiDung.MaQuyen);
            return View(nguoiDung);
        }

        // GET: Admin/User_63131848/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/User_63131848/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
            db.SaveChanges();
            return RedirectToAction("Index");
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

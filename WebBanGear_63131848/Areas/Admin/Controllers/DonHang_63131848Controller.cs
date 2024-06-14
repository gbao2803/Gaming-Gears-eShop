using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Areas.Admin.Controllers
{
    public class DonHang_63131848Controller : Controller
    {
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        // GET: Admin/DonHang_63131848
        public ActionResult XemChiTiet(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chiTietDonHangs = db.ChiTietDonHangs.Include(x => x.SanPham).Where(x => x.MaDH == id).ToList();
            if (chiTietDonHangs == null || !chiTietDonHangs.Any())
            {
                return HttpNotFound();
            }
            return View(chiTietDonHangs);
        }
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(s => s.NguoiDung);
            return View(donHangs.ToList());
        }
        [HttpGet]
        public ActionResult Index(string maDH = "", string maND = "", string tenND = "")
        {
            ViewBag.maDH = maDH;
            ViewBag.maND = maND;
            ViewBag.tenND = tenND;
            var donHangs = db.DonHangs.SqlQuery("TimKiemDH '" + maDH + "','" + maND + "', N'" + tenND + "'");
            if (donHangs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(donHangs.ToList());
        }

        string LayMaDH()
        {
            var maMax = db.DonHangs.ToList().Select(n => n.MaDH).Max();
            int maDH = int.Parse(maMax.Substring(2)) + 1;
            string DH = String.Concat("00", maDH.ToString());
            return "DH" + DH.Substring(maDH.ToString().Length - 1);
        }

        public ActionResult Create()
        {
            ViewBag.MaDH = LayMaDH();
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTenND");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaNguoiDung,DiaChiDH,NgayDat,TinhTrang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                donHang.MaDH = LayMaDH();
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTenND", donHang.MaNguoiDung);
            return View(donHang);
        }

        // GET: Admin/DonHang_63131848/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTenND", donHang.MaNguoiDung);
            return View(donHang);
        }

        // POST: Admin/DonHang_63131848/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaNguoiDung,DiaChiDH,NgayDat,TinhTrang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "HoTenND", donHang.MaNguoiDung);
            return View(donHang);
        }

        // GET: Admin/DonHang_63131848/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: Admin/DonHang_63131848/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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

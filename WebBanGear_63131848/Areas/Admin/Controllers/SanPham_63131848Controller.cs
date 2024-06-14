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
    public class SanPham_63131848Controller : Controller
    {
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        // GET: Admin/SanPham_63131848
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaCungCap);
            return View(sanPhams.ToList());
        }
        [HttpGet]
        public ActionResult Index(string maSP = "", string tenSP = "")
        {
            ViewBag.maSP = maSP;
            ViewBag.tenSP = tenSP;
            var sanPhams = db.SanPhams.SqlQuery("TimKiemSP '" + maSP + "', N'" + tenSP + "'");
            if (sanPhams.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(sanPhams.ToList());
        }
        string LayMaSP()
        {
            var maMax = db.SanPhams.ToList().Select(n => n.MaSP).Max();
            int maSP = int.Parse(maMax.Substring(2)) + 1;
            string SP = String.Concat("00", maSP.ToString());
            return "SP" + SP.Substring(maSP.ToString().Length - 1);
        }

        // GET: Admin/SanPham_63131848/Create
        public ActionResult Create()
        {   

            ViewBag.MaSP = LayMaSP();
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MoTa,GiaTien,SoLuong,TrangThai,MaNCC,MaLoaiSP,HinhAnh")] SanPham sanPham)
        {
            var imgSP = Request.Files["AnhSP"];
            string postedFileName = System.IO.Path.GetFileName(imgSP.FileName);
            var path = Server.MapPath("/HinhAnhSP/" + postedFileName);
            imgSP.SaveAs(path);
            if (ModelState.IsValid)
            {   
                sanPham.MaSP = LayMaSP();
                sanPham.HinhAnh = postedFileName;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: Admin/SanPham_63131848/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,GiaTien,SoLuong,TrangThai,MaNCC,MaLoaiSP,HinhAnh")] SanPham sanPham)
        {
            var imgSP = Request.Files["AnhSP"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgSP.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgSP.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            return View(sanPham);
        }

        // GET: Admin/SanPham_63131848/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham_63131848/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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

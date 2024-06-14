using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Areas.Admin.Controllers
{
    public class CTDH_63131848Controller : Controller
    {
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        // GET: Admin/CTDH_63131848
        public ActionResult Index()
        {
            var chiTietDonHangs = db.ChiTietDonHangs.Include(s => s.DonHang).Include(s => s.SanPham);
            return View(chiTietDonHangs.ToList());
        }
        [HttpGet]
        public ActionResult Index(string maDH = "", string maSP = "",string tenSP = "")
        {   
            ViewBag.maDH = maDH;
            ViewBag.maSP = maSP;
            ViewBag.tenSP = tenSP;
            var chiTietDonHangs = db.ChiTietDonHangs.SqlQuery("TimKiemCTDH '" + maDH + "', '" + maSP + "',N'" + tenSP + "'");
            if (chiTietDonHangs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(chiTietDonHangs.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaSP,SoLuong")] ChiTietDonHang chiTietDonHang)
        {
            // Set DonGia and ThanhTien
            var sanPham = db.SanPhams.Find(chiTietDonHang.MaSP);
            if (sanPham != null)
            {
                chiTietDonHang.DonGia = sanPham.GiaTien;
                chiTietDonHang.ThanhTien = chiTietDonHang.DonGia * chiTietDonHang.SoLuong;
            }

            // Kiểm tra nếu sản phẩm đã tồn tại trong đơn hàng thì báo lỗi
            if (db.ChiTietDonHangs.Any(ct => ct.MaDH == chiTietDonHang.MaDH && ct.MaSP == chiTietDonHang.MaSP))
            {
                ModelState.AddModelError("", "Sản phẩm đã tồn tại trong đơn hàng.");
            }

            if (ModelState.IsValid)
            {
                db.ChiTietDonHangs.Add(chiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        public ActionResult Edit(string maDH, string maSP)
        {
            if (maDH == null || maSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(maDH, maSP);

            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaSP,SoLuong")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                var existingCT = db.ChiTietDonHangs.Find(chiTietDonHang.MaDH, chiTietDonHang.MaSP);
                if (existingCT != null)
                {
                    existingCT.SoLuong = chiTietDonHang.SoLuong;
                    existingCT.ThanhTien = existingCT.DonGia * existingCT.SoLuong;

                    db.Entry(existingCT).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaDH", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        public ActionResult Delete(string maDH, string maSP)
        {
            if (maDH == null || maSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(maDH, maSP);

            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }

            return View(chiTietDonHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string maDH, string maSP)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(maDH, maSP);
            db.ChiTietDonHangs.Remove(chiTietDonHang);
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

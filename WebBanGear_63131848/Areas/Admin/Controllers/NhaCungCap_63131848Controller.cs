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
    public class NhaCungCap_63131848Controller : Controller
    {
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        public ActionResult Index()
        {
            return View(db.NhaCungCaps.ToList());
        }
        [HttpGet]
        public ActionResult Index(string maNCC = "", string tenNCC = "")
        {
            ViewBag.maNCC = maNCC;
            ViewBag.tenNCC = tenNCC;
            var nhaCC = db.NhaCungCaps.SqlQuery("TimKiemNCC '" + maNCC + "', N'" + tenNCC + "'");
            if (nhaCC.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(nhaCC.ToList());
        }

        string LayMaNCC()
        {
            var maMax = db.NhaCungCaps.ToList().Select(n => n.MaNCC).Max();
            int maNCC = 1; // Default value if maMax is null or if parsing fails

            if (!string.IsNullOrEmpty(maMax) && maMax.StartsWith("NCC"))
            {
                string numericPart = maMax.Substring(3); // Extract numeric part after "NCC"
                if (int.TryParse(numericPart, out int parsedMaNCC))
                {
                    maNCC = parsedMaNCC + 1;
                }
            }

            string ncc = String.Concat("00", maNCC.ToString());
            return "NCC" + ncc.Substring(maNCC.ToString().Length - 1);
        }

        public ActionResult Create()
        {
            ViewBag.MaNCC = LayMaNCC();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,EmailNCC,SDTNCC,DiaChiNCC")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                nhaCungCap.MaNCC = LayMaNCC();
                db.NhaCungCaps.Add(nhaCungCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCungCap);
        }

        // GET: Admin/NhaCungCap_63131848/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,EmailNCC,SDTNCC,DiaChiNCC")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCungCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCungCap);
        }

        // GET: Admin/NhaCungCap_63131848/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: Admin/NhaCungCap_63131848/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaCungCap nhaCungCap = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(nhaCungCap);
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

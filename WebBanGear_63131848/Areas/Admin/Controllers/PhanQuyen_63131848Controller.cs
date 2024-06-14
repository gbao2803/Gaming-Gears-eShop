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
    public class PhanQuyen_63131848Controller : Controller
    {
        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        // GET: Admin/PhanQuyen_63131848
        public ActionResult Index()
        {
            return View(db.PhanQuyens.ToList());
        }

            int LayMaQU()
            {
                int? maMax = db.PhanQuyens.Max(n => (int?)n.MaQuyen);
                int maQU = (maMax ?? 0) + 1;
                return maQU;
            }

        public ActionResult Create()
        {
            ViewBag.MaQuyen = LayMaQU();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuyen,TenQuyen")] PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                phanQuyen.MaQuyen = LayMaQU();
                db.PhanQuyens.Add(phanQuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = LayMaQU();
            return View(phanQuyen);
        }

        // GET: Admin/PhanQuyen_63131848/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            return View(phanQuyen);
        }

        // POST: Admin/PhanQuyen_63131848/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQuyen,TenQuyen")] PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanQuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phanQuyen);
        }

        // GET: Admin/PhanQuyen_63131848/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            return View(phanQuyen);
        }

        // POST: Admin/PhanQuyen_63131848/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            db.PhanQuyens.Remove(phanQuyen);
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

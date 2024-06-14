using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Controllers
{
    public class LoaiSP_63131848Controller : Controller
    {
        WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();
        // GET: LoaiSP_63131848

        
        public ActionResult LaptopPartial(string categoryName)
        {
            var lap = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "LAP").Take(5).ToList();
            return PartialView(lap);
        }
        public ActionResult MonitorPartial(string categoryName)
        {
            var mon = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "MON").Take(5).ToList();
            return PartialView(mon);
        }
        public ActionResult MousePartial(string categoryName)
        {
            var mou = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "MOU").Take(5).ToList();
            return PartialView(mou);
        }
        public ActionResult CPUPartial(string categoryName)
        {
            var cpu = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "CPU").Take(5).ToList();
            return PartialView(cpu);
        }
        public ActionResult RAMPartial(string categoryName)
        {
            var ram = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "RAM").Take(5).ToList();
            return PartialView(ram);
        }
        public ActionResult DiskPartial(string categoryName)
        {
            var disk = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "DISK").Take(5).ToList();
            return PartialView(disk);
        }
        public ActionResult MainPartial(string categoryName)
        {
            var main = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "MAIN").Take(5).ToList();
            return PartialView(main);
        }
        public ActionResult CardPartial(string categoryName)
        {
            var card = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "CARD").Take(5).ToList();
            return PartialView(card);
        }
        public ActionResult PSUPartial(string categoryName)
        {
            var psu = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "PSU").Take(5).ToList();
            return PartialView(psu);
        }
        public ActionResult CasePartial(string categoryName)
        {
            var cases = db.SanPhams.Include("LoaiSanPham").Include("NhaCungCap").Where(n => n.MaLoaiSP == "CASE").Take(5).ToList();
            return PartialView(cases);
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
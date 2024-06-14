using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        private readonly WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();
        public ActionResult Login(string tenDangNhapOrEmail, string matKhau)
        {
            var user = db.NguoiDungs.SingleOrDefault(u => (u.TenDangNhap.Equals(tenDangNhapOrEmail) || u.Email.Equals(tenDangNhapOrEmail)) && u.MatKhau == matKhau);
            if (user != null)
            {
                if (user.MaQuyen == 1)
                {
                    Session["admin"] = user;
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            ViewBag.Notification = "Nhập sai tên tài khoản/Email hoặc mật khẩu";
            return View();

        }

        public ActionResult DangXuat()
        {
            Session["admin"] = null;
            return RedirectToAction("Login", "AdminLogin");

        }

        string LayMaUser()
        {
            var maMax = db.NguoiDungs.ToList().Select(n => n.MaNguoiDung).Max();
            int maND = int.Parse(maMax.Substring(2)) + 1;
            string ND = String.Concat("00", maND.ToString());
            return "ND" + ND.Substring(maND.ToString().Length - 1);
        }
        public ActionResult Register()
        {
            return View();
        }
        //REGISTER ADMIN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(NguoiDung model, string xacNhan)
        {
            if (ModelState.IsValid)
            {
                var kiemTra = db.NguoiDungs.Any(x => x.Email == model.Email || x.TenDangNhap == model.TenDangNhap);
                if (kiemTra)
                {
                    ModelState.AddModelError("", "Tên tài khoản/Email đã tồn tại!");
                    return View(model);
                }
                if (string.IsNullOrEmpty(model.ReMatKhau))
                {
                    ModelState.AddModelError("", "Vui lòng nhập lại mật khẩu!");
                    return View(model);
                }
                if (model.MatKhau != model.ReMatKhau)
                {
                    ModelState.AddModelError("", "Mật khẩu không trùng khớp!");
                    return View(model);
                }
                model.MaNguoiDung = LayMaUser();
                model.MaQuyen = 1;
                db.NguoiDungs.Add(model);
                db.SaveChanges();
                var users = db.NguoiDungs.ToList();
                TempData["Notification"] = "Đăng ký thành công";
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
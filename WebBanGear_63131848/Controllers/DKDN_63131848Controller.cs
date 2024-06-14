using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Controllers
{
    public class DKDN_63131848Controller : Controller
    {

        private WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();
        
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult DangNhap(string tenDangNhapOrEmail, string matKhau)
        {
            var user = db.NguoiDungs.SingleOrDefault(u => (u.TenDangNhap.Equals(tenDangNhapOrEmail) || u.Email.Equals(tenDangNhapOrEmail)) && u.MatKhau == matKhau);
            if (user != null)
            {   

                if (user.MaQuyen == 3)
                {
                    Session["use"] = user;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Notification = "Nhập sai tên tài khoản/Email hoặc mật khẩu";
            return View();

        }

        string LayMaUser()
        {
            var maMax = db.NguoiDungs.ToList().Select(n => n.MaNguoiDung).Max();
            int maND = int.Parse(maMax.Substring(2)) + 1;
            string ND = String.Concat("00", maND.ToString());
            return "ND" + ND.Substring(maND.ToString().Length - 1);
        }
        public ActionResult DangKy()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DangKy(NguoiDung model, string xacNhan)
        {
            if (ModelState.IsValid)
            {   
                //kiểm tra đã tồn tại tài khoản hoặc email trong csdl chưa
                var userExists = db.NguoiDungs.Any(u => u.TenDangNhap == model.TenDangNhap || u.Email == model.Email);
                if (userExists)
                {
                    ModelState.AddModelError("", "Tên tài khoản/Email đã tồn tại");
                    return View(model);
                }
                if (string.IsNullOrEmpty(model.ReMatKhau))
                {
                    ModelState.AddModelError("", "Vui lòng nhập lại mật khẩu");
                    return View(model);
                }
                if (model.MatKhau != model.ReMatKhau)
                {
                    ModelState.AddModelError("", "Mật khẩu không trùng khớp");
                    return View(model);
                }
                model.MaNguoiDung = LayMaUser();
                model.MaQuyen = 3;

                db.NguoiDungs.Add(model);
                db.SaveChanges();

                var users = db.NguoiDungs.ToList();
                TempData["Notification"] = "Đăng ký thành công";
                return RedirectToAction("DangNhap");
                
            }

           
            return View(model);
        }



        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

    }
}
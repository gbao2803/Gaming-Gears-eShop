using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGear_63131848.Models;

namespace WebBanGear_63131848.Controllers
{
    public class ShoppingCart_63131848Controller : Controller
    {
        private readonly WebBanGear_63131848Entities db = new WebBanGear_63131848Entities();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            return View(cart);
        }

        public ActionResult CheckDN()
        {
            if(Session["use"] == null)
            {
                TempData["Notification"] = "Bạn cần đăng nhập để đặt hàng";
                return RedirectToAction("DangNhap","DKDN_63131848");
            }
            if (Session["Cart"] == null || ((Cart)Session["Cart"]).Items.Count == 0)
            {
                TempData["Notification"] = "Hãy thêm sản phẩm vào giỏ hàng";
                return RedirectToAction("Index","ShoppingCart_63131848");
            }
            else
            {
                return RedirectToAction("CheckOut", "ShoppingCart_63131848");
            }
        }
        public ActionResult CheckOut()
        {
            
            var cart = Session["Cart"] as Cart ?? new Cart();
            return View(cart);
        }


        public ActionResult PartialCheckOut() 
        {
            var cart = Session["Cart"] as Cart ?? new Cart();
            return PartialView(cart);
        }

        // GET: AddToCart
        public ActionResult AddToCart(string productId)
        {
            var product = db.SanPhams.FirstOrDefault(p => p.MaSP == productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cart = Session["Cart"] as Cart ?? new Cart();
            cart.AddItem(product, 1);
            Session["Cart"] = cart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(string productId)
        {
            var cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                cart.RemoveItem(productId);
                Session["Cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            var cart = Session["Cart"] as Cart;

            if (cart != null)
            {
                cart.Clear();
                Session["Cart"] = cart;
            }
            else
            {
                TempData["Message"] = "Giỏ hàng đã được làm trống";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCartQuantity(string productId, string action)
        {
            var cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(i => i.Product.MaSP == productId);
                if (item != null)
                {
                    if (action == "increment")
                    {
                        item.Quantity += 1;
                    }
                    else if (action == "decrement" && item.Quantity > 1)
                    {
                        item.Quantity -= 1;
                    }
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Index");
        }
        string LayMaDH()
        {
            var maMax = db.DonHangs.ToList().Select(n => n.MaDH).Max();
            int maDH = int.Parse(maMax.Substring(2)) + 1;
            string DH = String.Concat("00", maDH.ToString());
            return "DH" + DH.Substring(maDH.ToString().Length - 1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(string hoTen, string diaChi)
        {   
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(diaChi))
            {
                TempData["Notification"] = "Vui lòng nhập đầy đủ thông tin người đặt hàng.";
                return RedirectToAction("CheckOut", "ShoppingCart_63131848");
            }
            //lấy thông tin người dùng trong session
            var user = Session["use"] as NguoiDung;
            //lấy thông tin giỏ hàng trong session
            var cart = Session["Cart"] as Cart;
            //kiểm tra sản phẩm hết hàng
            var hetHang = new List<SanPham>();
            foreach(var item in cart.Items)
            {
                var product = db.SanPhams.FirstOrDefault(p => p.MaSP == item.Product.MaSP);
                if (product != null && product.TrangThai == false) 
                {
                    hetHang.Add(product);
                }
            }
            //thông báo người dùng nếu hết hàng
            if (hetHang.Any())
            {
                string message = "Các sản phẩm sau đã hết hàng:";
                foreach (var product in hetHang)
                {
                    message += $" {product.TenSP},";
                }
                message = message.TrimEnd(',');
                TempData["Notification"] = message;
                return RedirectToAction("CheckOut", "ShoppingCart_63131848");
            }
            var order = new DonHang{ MaDH = LayMaDH(), MaNguoiDung = user.MaNguoiDung, DiaChiDH = diaChi, NgayDat = DateTime.Now, TinhTrang = false}; // Mặc định là chờ xác nhận
            db.DonHangs.Add(order);
            db.SaveChanges();
            foreach (var item in cart.Items)
            {
                var product = db.SanPhams.FirstOrDefault(p => p.MaSP == item.Product.MaSP);
                if (product != null)
                {
                    var ctdh = new ChiTietDonHang{MaDH = order.MaDH, MaSP = product.MaSP, SoLuong = item.Quantity, DonGia = product.GiaTien, ThanhTien = item.Quantity * product.GiaTien};
                    product.SoLuong -= item.Quantity;
                    db.ChiTietDonHangs.Add(ctdh);
                }
            }
            db.SaveChanges();
            cart.Clear();
            Session["Cart"] = cart;
            TempData["Notification"] = "Đặt hàng thành công!";
            return RedirectToAction("Index", "ShoppingCart_63131848");
        }
    }
}
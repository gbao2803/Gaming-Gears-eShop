﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanGear_63131848.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Display(Name = "Mã đơn hàng")]
        public string MaDH { get; set; }
        [Display(Name = "Mã người dùng")]
        [Required(ErrorMessage = "Bạn cần phải nhập số lượng sản phẩm")]
        public string MaNguoiDung { get; set; }
        [Display(Name = "Địa chỉ đặt hàng")]
        [Required(ErrorMessage = "Bạn cần phải nhập địa chỉ đặt hàng")]
        public string DiaChiDH { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        [Required(ErrorMessage = "Bạn cần phải nhập ngày đặt hàng")]
        public Nullable<System.DateTime> NgayDat { get; set; }
        [Display(Name = "Tình trạng")]
        public Nullable<bool> TinhTrang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}

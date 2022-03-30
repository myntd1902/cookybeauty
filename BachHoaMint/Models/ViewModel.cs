using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BachHoaMint.Models
{
    public class ViewModel
    {
        public KhachHang khachhang { get; set; }
        public HoaDon hoadon { get; set; }
        public LoaiSP loaisp { get; set; }
        public Nhanvien nhanvien { get; set; }
        public CTHD cthd { get; set; }
        public SanPham sanpham { get; set; }
        public double Thanhtien { get; set; }

    }
}
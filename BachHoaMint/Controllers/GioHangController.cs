using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaMint.Models;
using System.Net;
using System.Net.Mail;

namespace BachHoaMint.Controllers
{
    public class GioHangController : Controller
    {
        private QLBHEntities db = new QLBHEntities();
        // GET: GioHang
        public ActionResult Index(string err = "")
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }

        public RedirectToRouteResult AddToCart (string MaSP)
        {
            if(Session["giohang"] == null) //chưa có giỏ hàng
            {//tạo
                Session["giohang"] = new List<CartItem>();
            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            if(giohang.FirstOrDefault(m=>m.MaSP == MaSP) == null) //sản phẩm chưa có trong giỏ hàng
            {
                SanPham sp = db.SanPhams.Find(MaSP);
                CartItem item = new CartItem();
                item.MaSP = MaSP;
                item.TenSP = sp.TenSP;
                item.DonGia = Convert.ToDouble(sp.Dongia);
                item.SoLuong = 1;
                giohang.Add(item);
            }
            else
            {
                CartItem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);
                item.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Update(string MaSP, int txtSoLuong)
        {
            
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            
            CartItem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if(item != null)
            {
                item.SoLuong = txtSoLuong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Delete(string MaSP)
        {

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;

            CartItem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Order(string Email, string Phone)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            string sMsg = "<html><body><table border='1'><caption><th>THÔNG TIN ĐẶT HÀNG</th></caption>" ;
            sMsg += "<tr><th>STT</th><th>Tên hàng</th><th>Đơn giá</th><th>Thành tiền</th></tr>";
            int i = 0;
            double tongtien = 0;
            foreach(var item in giohang)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>" + i.ToString() + "</td>";
                sMsg += "<td>" + item.TenSP + "</td>";
                sMsg += "<td>" + item.SoLuong + "</td>";
                sMsg += "<td>" + item.DonGia.ToString("#,###") + "</td>";
                sMsg += "<td>" + item.ThanhTien.ToString("#,###") + "</td>";
                sMsg += "</tr>";
                tongtien += item.ThanhTien;
            }
            sMsg += "<tr><th cospan='5'>Tổng cộng " + tongtien.ToString("#,### VNĐ") + "</th></tr>";
            //sMsg += "</table></body></html>";
            try
            {
                MailMessage mail = new MailMessage("snowwhite1234231@gmail.com", Email, "Thông tin đặt hàng", sMsg);
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("snowwhite1234231", "snowwhite1902");
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {

                //ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
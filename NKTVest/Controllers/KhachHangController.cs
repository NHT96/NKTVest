using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{
    public class KhachHangController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HOTENKH"];
            var ns = String.Format("{0:yyyy/MM/dd}", collection["NTNS"]);
            var dc = collection["DIACHI"];
            var dt = collection["SDTKH"];
            var email = collection["EMAILKH"];
            var pass = collection["PASSKH"];
            var repass = collection["REPASSKH"];
            if(String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng vui lòng không để trống!";
            }
            else if (String.IsNullOrEmpty(dc))
            {
                ViewData["Loi2"] = "Vui lòng điền địa chỉ chính xác để sau này nhận hàng!";
            }
            else if (String.IsNullOrEmpty(dt))
            {
                ViewData["Loi3"] = "Số điện thoại dùng để đăng nhập. Vui lòng không bỏ trống!";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi5"] = "Vui lòng nhập mật khẩu!";
            }
            else if (String.IsNullOrEmpty(repass))
            {
                ViewData["Loi6"] = "Vui lòng nhập lại mật khẩu!";
            }
            else
            {
                kh.HOTENKH = hoten;
                kh.NTNS = DateTime.Parse(ns);
                kh.DIACHI = dc;
                kh.SDTKH = dt;
                kh.EMAILKH = email;
                kh.PASSKH = repass;
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                ViewBag.ThongBao = "Đăng ký thành công!";
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var dt = collection["SDTKH"];
            var pass = collection["PASSKH"];
           if (String.IsNullOrEmpty(dt))
            {
                ViewData["Loi3"] = "Số điện thoại dùng để đăng nhập. Vui lòng không bỏ trống!";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["Loi5"] = "Vui lòng nhập mật khẩu!";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.SDTKH == dt && n.PASSKH == pass);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công!";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "Vest");
                }
                else
                    ViewBag.ThongBao = "Số điện thoại hoặc mật khẩu không chính xác!";
            }
            return View();
        }
        private string Name()
        {
            string name = "";
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if (kh != null)
            {
                name = kh.HOTENKH;
            }
            return name;
        }

        public ActionResult TenKH()
        {
            ViewBag.Name = Name();
            return PartialView();
        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("index", "Vest");
        }
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(FormCollection collection, FEEDBACK fb)
        {
            var hoten = collection["HOTENNG"];
            var sdt = collection["SDTNG"];
            var mail = collection["EMAILNG"];
            var noidung = collection["NOIDUNG"];
            var trangthai = collection["TRANGTHAI"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên vui lòng không được để trống";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                ViewData["Loi2"] = "Số điện thoại vui lòng không được để trống";
            }
            else if (String.IsNullOrEmpty(mail))
            {
                ViewData["Loi3"] = "Email vui lòng không được để trống";
            }
            else if (String.IsNullOrEmpty(noidung))
            {
                ViewData["Loi4"] = "Không được để trống";
            }
            else
            {
                fb.HOTENNG = hoten;
                fb.SDTNG = sdt;
                fb.EMAILNG = mail;
                fb.NOIDUNG = noidung;
                fb.TRANGTHAI = false;
                data.FEEDBACKs.InsertOnSubmit(fb);
                data.SubmitChanges();
                ViewBag.ThongBao = "Đã gửi phản hồi!";
                return RedirectToAction("LienHe");
            }
            return this.LienHe();
        }
        [HttpGet]
        public ActionResult TTCN()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("index", "Vest");
            }
            else
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
                return View(data.KHACHHANGs.SingleOrDefault(n => n.MAKH == kh.MAKH));
            }
        }
        [HttpPost, ActionName("TTCN")]
        public ActionResult TDTT()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("index", "Vest");
            }
            else
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
                var dp = data.KHACHHANGs.SingleOrDefault(n => n.MAKH == kh.MAKH);
                UpdateModel(dp);
                data.SubmitChanges();
                return RedirectToAction("index", "Vest");
            }
        }
    }
}
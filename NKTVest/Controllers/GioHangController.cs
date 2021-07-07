using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{
    public class GioHangController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: GioHang

        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGiohang(string gMasp, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.gMasp == gMasp);
            if (sanpham == null)
            {
                sanpham = new GioHang(gMasp);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.gSoluong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int Tongsoluong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                Tongsoluong = lstGiohang.Sum(n => n.gSoluong);
            }
            return Tongsoluong;
        }

        private int TongTien()
        {
            int Tongtien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                Tongtien = lstGiohang.Sum(n => n.gTongtien);
            }
            return Tongtien;
        }


        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Vest");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGioHang(string msp)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sp = lstGiohang.SingleOrDefault(n => n.gMasp == msp);
            if(sp!=null)
            {
                lstGiohang.RemoveAll(n => n.gMasp == msp);
                return RedirectToAction("GioHang");
            }
            if(lstGiohang.Count==0)
            {
                return RedirectToAction("Index", "Vest");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(string msp, FormCollection f)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sp = lstGiohang.SingleOrDefault(n => n.gMasp == msp);
            if (sp != null)
            {
                sp.gSoluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Vest");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() =="")
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Vest");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            DATHANG ddh = new DATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> gh = Laygiohang();
            ddh.MAKH = kh.MAKH;
            ddh.NGAYDAT = DateTime.Now;
            var ngaygiao = String.Format("{0:yyyy/MM/dd}", collection["NgayGiao"]);
            ddh.NGAYGIAO = DateTime.Parse(ngaygiao);
            ddh.THANHTOAN = false;
            ddh.GIAOHANG = false;
            data.DATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();

            foreach(var item in gh)
            {
                CTDDH ctdh = new CTDDH();
                ctdh.MADH = ddh.MADH;
                ctdh.MASP = item.gMasp;
                ctdh.SOLUONG = item.gSoluong;
                ctdh.GIABAN = item.gDongia;
                ctdh.TONGTIEN = item.gDongia * item.gSoluong;
                data.CTDDHs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDH", "GioHang");
        }
        public ActionResult XacNhanDH()
        {
            return View();
        }
    }
}
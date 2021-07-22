using NKTVest.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{
    public class AdminController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: Admin
        private List<SANPHAM> DSSPmoi(int n)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NGAYTHEM).Take(n).ToList();
        }
        public ActionResult Index(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                int pagesize = 6;
                int pagenum = (page ?? 1);
                var spm = DSSPmoi(24);
                return View(spm.ToPagedList(pagenum, pagesize));
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var mnv = collection["username"];
            var pas = collection["pass"];
            NHANVIEN nv = data.NHANVIENs.SingleOrDefault(n => n.MANV == mnv && n.PASS == pas);
            if (nv != null)
            {
                Session["TKAdmin"] = nv;
                return RedirectToAction("index", "Admin");
            }
            else
                ViewBag.Thongbao = "Sai thông tin đăng nhập!";
            return View();
        }
        public ActionResult QuenMK()
        {
            return View();
        }
        private string Name()
        {
            string name ="";
            NHANVIEN nv = Session["TKAdmin"] as NHANVIEN;
            if (nv != null)
            {
                name = nv.HOTENNV;
            }
            return name;
        }

        public ActionResult TenNguoiDung()
        {
            ViewBag.Name = Name();
            return PartialView();
        }
        public ActionResult Logout()
        {
            Session["TKAdmin"] = null;
            return RedirectToAction("Login");
        }
    }
}
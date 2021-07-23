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

        [HttpGet]
        public ActionResult DMK(string mnv)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                NHANVIEN nv = Session["TKAdmin"] as NHANVIEN;
                return View(data.NHANVIENs.SingleOrDefault(n=>n.MANV==nv.MANV));
            }
        }
        [HttpPost, ActionName("DMK")]
        public ActionResult XNDMK(string mnv, FormCollection collection)
        {
            var pass = collection["pass"];
            var newpass = collection["newpass"];
            NHANVIEN v = Session["TKAdmin"] as NHANVIEN;
            var nv = data.NHANVIENs.SingleOrDefault(n => n.MANV == v.MANV);
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (nv.PASS.Trim()!=pass)
                {
                    ViewData["loi1"] = "Sai Mật Khẩu!";
                    return View();
                }
                else if(newpass.Length>15 || newpass.Length<6)
                {
                    ViewData["loi2"] = "6<=MK<=15!";
                    return View();
                }
                else
                {
                    nv.PASS = newpass;
                    UpdateModel(nv);
                    data.SubmitChanges();
                    return RedirectToAction("index", "Admin");
                }
                
            }
        }
        [HttpGet]
        public ActionResult TTCN(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                NHANVIEN nv = Session["TKAdmin"] as NHANVIEN;
                return View(data.NHANVIENs.SingleOrDefault(n => n.MANV == nv.MANV));
            }
        }
        [HttpPost, ActionName("TTCN")]
        public ActionResult TDTT(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                NHANVIEN nv = Session["TKAdmin"] as NHANVIEN;
                var dp = data.NHANVIENs.SingleOrDefault(n => n.MANV == nv.MANV);
                UpdateModel(dp);
                data.SubmitChanges();
                return RedirectToAction("index", "Admin");
            }
        }
    }
}
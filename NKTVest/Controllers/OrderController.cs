using NKTVest.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{
    public class OrderController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: Order
        public ActionResult Index(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 9;
                int pagenum = (page ?? 1);
                var ddh = data.DATHANGs.OrderByDescending(a => a.NGAYDAT).ToList();
                return View(ddh.ToPagedList(pagenum, pagesize));
            }
        }

        public ActionResult CGH(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 9;
                int pagenum = (page ?? 1);
                var ddh = data.DATHANGs.OrderByDescending(a => a.NGAYDAT).Where(d=>d.GIAOHANG==false).ToList();
                return View(ddh.ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult DGH(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 9;
                int pagenum = (page ?? 1);
                var ddh = data.DATHANGs.OrderByDescending(a => a.NGAYDAT).Where(d => d.GIAOHANG == true).ToList();
                return View(ddh.ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult CTT(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 9;
                int pagenum = (page ?? 1);
                var ddh = data.DATHANGs.OrderByDescending(a => a.NGAYDAT).Where(d => d.THANHTOAN == false).ToList();
                return View(ddh.ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult DTT(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 9;
                int pagenum = (page ?? 1);
                var ddh = data.DATHANGs.OrderByDescending(a => a.NGAYDAT).Where(d => d.THANHTOAN == true).ToList();
                return View(ddh.ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var dh = data.DATHANGs.SingleOrDefault(l => l.MADH == id);
                return View(dh);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var dh = data.DATHANGs.SingleOrDefault(l => l.MADH == id);
                return View(dh);
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult XNS(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                NHANVIEN nv = Session["TKAdmin"] as NHANVIEN;
                var ddh = data.DATHANGs.Where(l => l.MADH == id).SingleOrDefault();
                ddh.THANHTOAN = true;
                ddh.GIAOHANG = true;
                ddh.MANV = nv.MANV;
                UpdateModel(ddh);
                data.SubmitChanges();
                return RedirectToAction("index", "Order");
            }
        }
    }
}
using NKTVest.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{
    public class FeedBackController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: FeedBack
        public ActionResult Index(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var fb = data.FEEDBACKs.ToList();
                int pagesize = 10;
                int pagenum = (page ?? 1);
                return View(fb.ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult NotResponse(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var fb = data.FEEDBACKs.Where(f=>f.TRANGTHAI==false).ToList();
                int pagesize = 10;
                int pagenum = (page ?? 1);
                return View(fb.ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult Responded(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var fb = data.FEEDBACKs.Where(f => f.TRANGTHAI == true).ToList();
                int pagesize = 10;
                int pagenum = (page ?? 1);
                return View(fb.ToPagedList(pagenum, pagesize));
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
                return View(data.FEEDBACKs.SingleOrDefault(f => f.MAFB == id));
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
                var fb = data.FEEDBACKs.SingleOrDefault(l => l.MAFB == id);
                return View(fb);
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
                var fb = data.FEEDBACKs.Where(l => l.MAFB == id).SingleOrDefault();
                fb.TRANGTHAI = true;
                fb.MANV = nv.MANV;
                UpdateModel(fb);
                data.SubmitChanges();
                return RedirectToAction("index", "FeedBack");
            }
        }
    }
}
using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NKTVest.Controllers
{
    public class SanPhamController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: Admin
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
                var spm =data.SANPHAMs.ToList();
                return View(spm.ToPagedList(pagenum, pagesize));
            }
        }
    }
}
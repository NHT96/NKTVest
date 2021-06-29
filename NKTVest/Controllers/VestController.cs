using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{
    public class VestController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: Vest
        private List<SANPHAM> DSSPmoi(int n)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NGAYTHEM).Take(n).ToList();
        }
        public ActionResult Index()
        {
            var spm = DSSPmoi(10);
            return View(spm);
        }
        public ActionResult LoaiSP()
        {
            var lsp = from l in data.LOAISPs select l;
            return PartialView(lsp);
        }
        public ActionResult SPTheoLoai(string id)
        {
            var sp = from a in data.SANPHAMs where a.MALOAI==id select a;
            return View(sp);
        }
        public ActionResult Details(string id)
        {
            var sp = from a in data.SANPHAMs where a.MASP == id select a;
            return View(sp.Single());
        }
    }
}
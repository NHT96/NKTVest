using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace NKTVest.Controllers
{
    public class VestController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: Vest
        private List<SANPHAM> DSSPmoi(int n)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NGAYTHEM).Where(t=>t.TRANGTHAI==true).Take(n).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pagesize = 6;
            int pagenum = (page ?? 1);
            var spm = DSSPmoi(24);
            return View(spm.ToPagedList(pagenum,pagesize));
        }
        public ActionResult LoaiSP()
        {
            var lsp = from l in data.LOAISPs where l.TRANGTHAI == true select l;
            return PartialView(lsp);
        }
        public ActionResult SPTheoLoai(string id, int? page)
        {
            int pagesize = 6;
            int pagenum = (page ?? 1);
            var sp = from a in data.SANPHAMs where a.MALOAI==id && a.TRANGTHAI == true select a;
            return View(sp.ToPagedList(pagenum, pagesize));
        }
        public ActionResult Details(string id)
        {
            var sp = from a in data.SANPHAMs where a.MASP == id select a;
            return View(sp.Single());
        }
        public ActionResult CSBH()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TimKiem(string search)
        {
            var sp = from a in data.SANPHAMs where a.TENSP.Contains(search) && a.TRANGTHAI==true select a;
            return View(sp);
        }
    }
}
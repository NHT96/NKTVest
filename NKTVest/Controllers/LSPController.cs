using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKTVest.Controllers
{

    public class LSPController : Controller
    {
        NKTVDataContext data = new NKTVDataContext();
        // GET: LSP
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(data.LOAISPs.ToList());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create( LOAISP lsp)
        {
            data.LOAISPs.InsertOnSubmit(lsp);
            data.SubmitChanges();
            return RedirectToAction("index");
        }
    }
}
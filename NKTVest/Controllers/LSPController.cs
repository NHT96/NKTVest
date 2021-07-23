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

        public ActionResult TrangThai()
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
            var lsps = data.LOAISPs.FirstOrDefault(l => l.MALOAI == lsp.MALOAI);
            if (lsps != null)
            {
                ViewData["loi1"] = "Mã loại sản phẩm này đã tồn tại";
            }
            else if (lsp.MALOAI.Length != 6)
            {
                ViewData["loi2"] = "Nhập đúng 06 ký tự cho mã loại sản phẩm";
            }
            else
            {
                data.LOAISPs.InsertOnSubmit(lsp);
                data.SubmitChanges();
                return RedirectToAction("index");
            }
            return this.Create();
        }
        public ActionResult Details(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.SingleOrDefault(l => l.MALOAI == id);
                return View(lsp);
            }
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.SingleOrDefault(l => l.MALOAI == id);
                return View(lsp);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult XNX(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.Where(l => l.MALOAI == id).SingleOrDefault();
                data.LOAISPs.DeleteOnSubmit(lsp);
                data.SubmitChanges();
                return RedirectToAction("index", "LSP");
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.SingleOrDefault(l => l.MALOAI == id);
                return View(lsp);
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult XNS(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.Where(l => l.MALOAI == id).SingleOrDefault();
                UpdateModel(lsp);
                data.SubmitChanges();
                return RedirectToAction("index", "LSP");
            }
        }
        public ActionResult Hide(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.Where(l => l.MALOAI == id).SingleOrDefault();
                lsp.TRANGTHAI = false;
                UpdateModel(lsp);
                data.SubmitChanges();
                return RedirectToAction("TrangThai", "LSP");
            }
        }
        public ActionResult Show(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var lsp = data.LOAISPs.Where(l => l.MALOAI == id).SingleOrDefault();
                lsp.TRANGTHAI = true;
                UpdateModel(lsp);
                data.SubmitChanges();
                return RedirectToAction("TrangThai", "LSP");
            }
        }
    }
}
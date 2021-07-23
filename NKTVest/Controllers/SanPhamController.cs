using NKTVest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
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
        public ActionResult TrangThai(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 9;
                int pagenum = (page ?? 1);
                var spm = data.SANPHAMs.ToList();
                return View(spm.ToPagedList(pagenum, pagesize));
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
                var sp = data.SANPHAMs.Where(l => l.MASP == id).SingleOrDefault();
                sp.TRANGTHAI = false;
                UpdateModel(sp);
                data.SubmitChanges();
                return RedirectToAction("TrangThai", "SanPham");
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
                var sp = data.SANPHAMs.Where(l => l.MASP == id).SingleOrDefault();
                sp.TRANGTHAI = true;
                UpdateModel(sp);
                data.SubmitChanges();
                return RedirectToAction("TrangThai", "SanPham");
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
                ViewBag.MALOAI = new SelectList(data.LOAISPs.ToList().OrderBy(l => l.TENLOAI), "MALOAI", "TENLOAI");
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, HttpPostedFileBase fileupload)
        {
            var sps = data.SANPHAMs.FirstOrDefault(l => l.MASP == sp.MASP);
            ViewBag.MALOAI = new SelectList(data.LOAISPs.ToList().OrderBy(l => l.TENLOAI), "MALOAI", "TENLOAI");
             if (sps != null)
            {
                ViewData["loi1"] = "Mã sản phẩm này đã tồn tại";
            }
            else if (sp.MASP.Length != 6)
            {
                ViewData["loi2"] = "Nhập đúng 06 ký tự cho mã sản phẩm";
            }
            else if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/anhbia/th"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sp.ANHBIA = fileName;
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
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
                var sp = data.SANPHAMs.SingleOrDefault(l => l.MASP == id);
                return View(sp);
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
                var sp = data.SANPHAMs.SingleOrDefault(l => l.MASP == id);
                return View(sp);
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
                var sp = data.SANPHAMs.SingleOrDefault(l => l.MASP == id);
                data.SANPHAMs.DeleteOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("index", "SanPham");
            }
        }

        public ActionResult Edit(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                SANPHAM sp = data.SANPHAMs.SingleOrDefault(s => s.MASP == id);
                ViewBag.MALOAI = new SelectList(data.LOAISPs.ToList().OrderBy(l => l.TENLOAI), "MALOAI", "TENLOAI", sp.MALOAI);
                return View(sp);
            }
        }
        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult XNS(string id, HttpPostedFileBase fileupload)
        {
            ViewBag.MALOAI = new SelectList(data.LOAISPs.ToList().OrderBy(l => l.TENLOAI), "MALOAI", "TENLOAI");
            var sp = data.SANPHAMs.SingleOrDefault(s=>s.MASP==id);
            if (fileupload == null)
            {
                UpdateModel(sp);
                data.SubmitChanges();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/anhbia/th"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sp.ANHBIA = fileName;
                    UpdateModel(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
    }
}
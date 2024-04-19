using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using System.IO;
using System.Xml.Linq;
using PagedList;

namespace PBL3.Areas.Admin.Controllers
{
    public class SanPhamsController : LoginManagerController
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/SanPhams
        public ActionResult Index(string Searchtxt,string currentFilter, int? page)
        {
            
            if (Searchtxt != null)
            {
                page = 1;
            }
            else
            {
                Searchtxt = currentFilter;
            }

            ViewBag.CurrentFilter = Searchtxt;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (Searchtxt == null)
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai).ToList();
                return View(sanPhams.ToPagedList((int)pageNumber,(int)pageSize));
            }
            else
            {
                var sanPham = db.SanPhams.Where(p => p.TenSP.Contains(Searchtxt)).ToList();
                return View(sanPham.ToPagedList((int)pageNumber, (int)pageSize));
            }
            
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc");
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SP,ID_Danhmuc,TenSP,ID_KM,SoLuong,GiaBan,NhanHieuSP,MauSP,MoTaSP,Status")] SanPham sanPham, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh == null)
                {
                    sanPham.Anh = "";
                }    
                else if(uploadhinh != null || uploadhinh.ContentLength >0)
                {
                    string hinh = uploadhinh.FileName.ToString();
                    sanPham.Anh = hinh;
                }
                sanPham.Status = 1;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                    
                return RedirectToAction("Index");
            }

            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc", sanPham.ID_Danhmuc);
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM", sanPham.ID_KM);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc", sanPham.ID_Danhmuc);
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM", sanPham.ID_KM);
            ViewBag.Anh = sanPham.Anh;
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SP,ID_Danhmuc,TenSP,ID_KM,SoLuong,GiaBan,NhanHieuSP,MauSP,MoTaSP,Anh,Status")] SanPham sanPham, HttpPostedFileBase uploadhinh)
        {
            if (uploadhinh == null)
            {
                sanPham.Anh = sanPham.Anh;
            }
            else if (uploadhinh != null || uploadhinh.ContentLength > 0)
            {
                string hinh = uploadhinh.FileName.ToString();
                var path = Path.Combine(Server.MapPath("~/Anh"), hinh);
                uploadhinh.SaveAs(path);
                sanPham.Anh = hinh;
            }
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Danhmuc = new SelectList(db.DanhMucSPs, "ID_Danhmuc", "TenDanhMuc", sanPham.ID_Danhmuc);
            ViewBag.ID_KM = new SelectList(db.KhuyenMais, "ID_KM", "NoiDungKM", sanPham.ID_KM);
            return View(sanPham);
        }
        public ActionResult Trash(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return RedirectToAction("Index");
            }
            sanPham.Status = 0;
            db.Entry(sanPham).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

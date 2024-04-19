using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    public class DanhMucSPsController : LoginManagerController
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/DanhMucSPs
        public ActionResult Index()
        {
            return View(db.DanhMucSPs.ToList());
        }
        // GET: Admin/DanhMucSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Danhmuc,TenDanhMuc,Status")] DanhMucSP danhMucSP)
        {
            if (ModelState.IsValid)
            {
                danhMucSP.Status = 1;
                db.DanhMucSPs.Add(danhMucSP);
                db.SaveChanges();
                List<String> list = new List<String>();
                foreach (var lin in db.DanhMucSPs.Where(p => p.Status == 1).ToList())
                {
                    list.Add(lin.TenDanhMuc);
                }
                Session["ListDanhMuc"] = list;
                return RedirectToAction("Index");
            }

            return View(danhMucSP);
        }

        // GET: Admin/DanhMucSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucSP danhMucSP = db.DanhMucSPs.Find(id);
            if (danhMucSP == null)
            {
                return HttpNotFound();
            }
            return View(danhMucSP);
        }

        // POST: Admin/DanhMucSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Danhmuc,TenDanhMuc,Status")] DanhMucSP danhMucSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMucSP).State = EntityState.Modified;
                db.SaveChanges();
                List<String> list = new List<String>();
                foreach (var lin in db.DanhMucSPs.Where(p => p.Status == 1).ToList())
                {
                    list.Add(lin.TenDanhMuc);
                }
                Session["ListDanhMuc"] = list;
                return RedirectToAction("Index");
            }
            return View(danhMucSP);
        }
        public ActionResult Trash(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DanhMucSP danhMucSP = db.DanhMucSPs.Find(id);
            if (danhMucSP == null)
            {
                return RedirectToAction("Index");
            }
            danhMucSP.Status = 0;
            db.Entry(danhMucSP).State = EntityState.Modified;
            db.SaveChanges();
            List<String> list = new List<String>();
            foreach (var lin in db.DanhMucSPs.Where(p => p.Status == 1).ToList())
            {
                list.Add(lin.TenDanhMuc);
            }
            Session["ListDanhMuc"] = list;
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

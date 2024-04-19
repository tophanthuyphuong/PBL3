using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;

namespace PBL3.Areas.Admin.Controllers
{
    public class KhuyenMaisController : LoginManagerController
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/KhuyenMais
        public ActionResult Index()
        {
            return View(db.KhuyenMais.ToList());
        }

        // GET: Admin/KhuyenMais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_KM,BatDauKM,KetThucKM,NoiDungKM,LoaiKM,Status")] KhuyenMai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                if (khuyenMai.KetThucKM < DateTime.Now)
                {
                    ViewBag.Error = "<p class='text-danger'> " + " Ngày kết thúc lớn hơn ngày hôm nay" + "</p>";
                    return View();

                }
                else if(khuyenMai.KetThucKM.Value.Year < 1900 || khuyenMai.BatDauKM.Value.Year < 1900)
                {
                    ViewBag.Error = "<p class='text-danger'> " + " Ngày không hợp lệ" + "</p>";
                    return View();
                }
                else if (khuyenMai.KetThucKM < khuyenMai.BatDauKM)
                {
                    ViewBag.Error = "<p class='text-danger'> " + " Ngày kết thúc nhỏ hơn ngày bắt đầu" + "</p>";
                    return View();
                }
                else
                {
                    khuyenMai.Status = 1;
                    db.KhuyenMais.Add(khuyenMai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View(khuyenMai);
        }

        // GET: Admin/KhuyenMais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            ViewBag.Start = khuyenMai.BatDauKM;
            ViewBag.End = khuyenMai.KetThucKM;
            return View(khuyenMai);
        }

        // POST: Admin/KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID_KM,BatDauKM,KetThucKM,NoiDungKM,LoaiKM,Status")] KhuyenMai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                if (khuyenMai.KetThucKM < DateTime.Now)
                {
                    ViewBag.Error = "<p class='text-danger'> " + " Ngày kết thúc lớn hơn ngày hôm nay" + "</p>";
                    return View(khuyenMai);

                }
                else if (khuyenMai.KetThucKM.Value.Year < 1900 || khuyenMai.BatDauKM.Value.Year < 1900)
                {
                    ViewBag.Error = "<p class='text-danger'> " + " Ngày không hợp lệ" + "</p>";
                    return View();
                }
                else if (khuyenMai.KetThucKM < khuyenMai.BatDauKM)
                {
                    ViewBag.Error = "<p class='text-danger'> " + " Ngày kết thúc nhỏ hơn ngày bắt đầu" + "</p>";
                    return View(khuyenMai);
                }
                else
                {
                    db.Entry(khuyenMai).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(khuyenMai);
        }
        public ActionResult Trash(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return RedirectToAction("Index");
            }
            khuyenMai.Status = 0;
            db.Entry(khuyenMai).State = EntityState.Modified;
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

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
    public class ChiTietHoaDonsController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/ChiTietHoaDons
        public ActionResult Index(int? id)
        {
            var chiTietHoaDons = db.ChiTietHoaDons.Where(c =>c.ID_HoaDon == id).Include(c => c.HoaDon).Include(c => c.SanPham);
            return View(chiTietHoaDons.ToList());
        }

        // GET: Admin/ChiTietHoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Where(c => c.ID_SP == id).FirstOrDefault();
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
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

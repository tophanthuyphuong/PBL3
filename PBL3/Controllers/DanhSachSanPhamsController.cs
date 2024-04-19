using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using PagedList;
using System.Data.SqlClient;

namespace PBL3.Controllers
{
    public class DanhSachSanPhamsController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: DanhSachSanPhams
        public ActionResult Index(string DanhMucsp,string currentFilter, string Searchtxt, int? page)
        {
            ViewBag.DanhMuc = DanhMucsp;
            if (Searchtxt != null)
            {
                page = 1;
            }
            else
            {
                Searchtxt = currentFilter;
            }

            ViewBag.CurrentFilter = Searchtxt;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (DanhMucsp == null)
            {
                if (Searchtxt == null)
                {
                    var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai).OrderBy(s => s.ID_SP).ToList();
                    return View(sanPhams.ToPagedList((int)pageNumber, (int)pageSize));
                }
                else
                {
                    var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai)
                        .Where(s => s.TenSP.Contains(Searchtxt)).OrderBy(s => s.ID_SP).ToList();
                    return View(sanPhams.ToPagedList((int)pageNumber, (int)pageSize));
                }
            }
            else
            {
                if (Searchtxt == null)
                {
                    var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai)
                        .Where(s => s.DanhMucSP.TenDanhMuc == DanhMucsp).OrderBy(s =>s.ID_SP).ToList();
                    return View(sanPhams.ToPagedList((int)pageNumber, (int)pageSize));
                }
                else
                {
                    var sanPhams = db.SanPhams.Include(s => s.DanhMucSP).Include(s => s.KhuyenMai)
                        .Where(s => s.TenSP.Contains(Searchtxt) && s.DanhMucSP.TenDanhMuc == DanhMucsp).OrderBy(s => s.ID_SP).ToList();
                    return View(sanPhams.ToPagedList((int)pageNumber, (int)pageSize));
                }
            }
        }

        // GET: DanhSachSanPhams/Details/5
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

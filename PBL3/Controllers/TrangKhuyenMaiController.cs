using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;
using PBL3.Models.Model_View;
using System.IO;
using System.Xml.Linq;
using PagedList;

namespace PBL3.Controllers
{
    public class TrangKhuyenMaiController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        // GET: TrangKhuyenMai
        public ActionResult Index()
        {

            TrangKhuyenMai List = new TrangKhuyenMai();
            List<SanPham> sanphams = db.SanPhams.Where(p => p.KhuyenMai.Status == 1).ToList();
            List<KhuyenMai> khuyenmais = db.KhuyenMais.Where(p => p.Status == 1 && p.NoiDungKM != "0").ToList();
            List.ListSP = sanphams;
            List.ListKM = khuyenmais;
            return View(List);
        }
       
    }
}
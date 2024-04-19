using PBL3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Linq;

namespace PBL3.Controllers
{
    public class HomeController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMucSP);
            return View(sanPhams.ToList());
            
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}
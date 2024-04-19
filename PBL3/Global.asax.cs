using PBL3.Models;
using PBL3.Models.Model_View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PBL3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            Session["UserName"] = "";
            Session["Password"] = "";
            Session["Quyen"] = "";
            Session["Name"] = "";
            Session["ID_Account"] = "";

            List<String> list = new List<String>();
            foreach (var lin in db.DanhMucSPs.Where(p =>p.Status == 1).ToList())
            {
                list.Add(lin.TenDanhMuc);
            }    
            Session["ListDanhMuc"] = list;
            var listKM = db.KhuyenMais.Where(p =>p.KetThucKM < DateTime.Now).ToList();
            foreach (var km in listKM)
            {
                km.Status = 0;
                db.Entry(km).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            
        }
    }
}

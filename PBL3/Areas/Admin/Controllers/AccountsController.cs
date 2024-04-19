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
    public class AccountsController : LoginManagerController
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();

        // GET: Admin/Accounts
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.User);
            return View(accounts.ToList());
        }
        public ActionResult Change(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Account acc = db.Accounts.Find(id);
            if (acc == null)
            {
                return RedirectToAction("Index");
            }
            if(acc.Quyen == 0)
            {
                acc.Quyen = 1;
            }
            else
            {
                acc.Quyen = 0;
            }    
            db.Entry(acc).State = EntityState.Modified;
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

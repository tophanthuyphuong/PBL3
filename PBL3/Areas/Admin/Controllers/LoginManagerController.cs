using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3.Areas.Admin.Controllers
{
    public class LoginManagerController : Controller
    {
        // GET: Admin/LoginManager
        public LoginManagerController()
        {
            if (System.Web.HttpContext.Current.Session["Quyen"].Equals("0") == false)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/HomePage/Login");
            }
        }
    }
}
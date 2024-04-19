using PBL3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PBL3.Models.Model_View;

namespace PBL3.Areas.Admin.Controllers
{
    public class HomePageController : Controller
    {
        // GET: Admin/HomePage
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        public ActionResult Index()
        {
            List<HoaDon> thongketong = db.HoaDons.Where(p => p.Status == 1).ToList();
            List<HoaDon> thongkenam = db.HoaDons.Where(p => p.Status == 1 && p.NgayBan.Value.Year == DateTime.Now.Year).ToList();
            List<HoaDon> thongkenamtruoc = db.HoaDons.Where(p => p.Status == 1 && p.NgayBan.Value.Year == DateTime.Now.Year - 1).ToList();
            List<HoaDon> thongkengay = db.HoaDons.Where(p => p.Status == 1 && p.NgayBan.Value.Year == DateTime.Now.Year
            && p.NgayBan.Value.Month == DateTime.Now.Month && p.NgayBan.Value.Day == DateTime.Now.Day).ToList();
            int sohoadon = db.HoaDons.Where(p => p.Status == 0).Count();
            ViewBag.yeartotal = Thongke(thongkenam);
            ViewBag.preyeartotal = Thongke(thongkenamtruoc);
            ViewBag.datetotal = Thongke(thongkengay);
            ViewBag.total = Thongke(thongketong);
            ViewBag.sohoadon = sohoadon;
            var model = db.ChiTietHoaDons.Include(p => p.SanPham).Where(p => p.HoaDon.Status == 1).GroupBy(p => p.ID_SP)
                .Select(g => new ThongKeSanPham
                {
                    ID_SP= g.Key,
                    TenSP = db.SanPhams.FirstOrDefault(sp => sp.ID_SP == g.Key).TenSP,
                    SoLuong = (int)g.Sum(p => p.SoLuong)
                }).OrderByDescending(p => p.SoLuong)
        .ToList();
            return View(model.ToList());
        }

        public double Thongke(List<HoaDon> thongketong)
        {
            double total = 0.0;
            foreach (var thongke in thongketong)
            {
                var cthd = db.ChiTietHoaDons.Where(p => p.ID_HoaDon == thongke.ID_HoaDon)
                    .Include(p => p.SanPham);
                foreach (var item in cthd)
                {
                    if (item.SanPham.KhuyenMai.LoaiKM == 0 && thongke.NgayBan <= item.SanPham.KhuyenMai.KetThucKM)
                    {
                        total += Convert.ToDouble(item.SoLuong * (item.SanPham.GiaBan - Convert.ToDouble(item.SanPham.KhuyenMai.NoiDungKM)));
                    }
                    else if (item.SanPham.KhuyenMai.LoaiKM == 1 && thongke.NgayBan <= item.SanPham.KhuyenMai.KetThucKM)
                        total += Convert.ToDouble(item.SoLuong * (item.SanPham.GiaBan * (1 - Convert.ToDouble(item.SanPham.KhuyenMai.NoiDungKM) / 100)));
                    else
                        total += Convert.ToDouble(item.SoLuong * item.SanPham.GiaBan);
                }
            }
            return total;
        }
        public ActionResult Login()
        {
            if (Session["Quyen"].Equals("0") == true)
            {
                Response.Redirect("~/Admin/HomePage");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            Account acc = db.Accounts.FirstOrDefault(x => x.Username == username && x.Quyen == 0);
            string err = "";
            if (acc != null)
            {
                if (acc.Password.Replace(" ", "") == password)
                {
                    Session["UserName"] = acc.Username;
                    Session["Password"] = acc.Password;
                    Session["Quyen"] = acc.Quyen.ToString();
                    Session["ID_Account"] = acc.ID_Account.ToString();
                    Response.Redirect("~/Admin/HomePage");
                }
                else ViewBag.Error = "<p class='text-danger'> " + "Mật khẩu không hợp lệ" + "</p>";
            }
            else
            {
                ViewBag.Error = "<p class='text-danger'> " + "Tên đăng nhập không hợp lệ" + "</p>";
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session["UserName"] = "";
            Session["Password"] = "";
            Session["Quyen"] = "";
            Session["ID_Account"] = "";
            Session["GioHang"] = null;
            Session["Name"] = "";
            Response.Redirect("~/Admin/HomePage/Login");
            return null;
        }
    }
}
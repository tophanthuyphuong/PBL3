using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PBL3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "Dang-nhap",
                url: "dang-nhap",
                defaults: new { controller = "DangNhapDangKy", action = "DangNhap", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Dang-ky",
                url: "dang-ky",
                defaults: new { controller = "DangNhapDangKy", action = "DangKy", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Doi-mat-khau",
                url: "doi-mat-khau",
                defaults: new { controller = "DangNhapDangKy", action = "DoiMatKhau", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Dang-xuat",
                url: "dang-xuat",
                defaults: new { controller = "DangNhapDangKy", action = "DangXuat", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Gio-hang",
                url: "Gio-hang",
                defaults: new { controller = "GioHangs", action = "XemGioHang", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Thong-tin-ca-nhan",
                url: "Thong-tin-ca-nhan",
                defaults: new { controller = "DangNhapDangKy", action = "ThongTinCaNhan", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Trang-chu",
                url: "Trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Thay-doi-thong-tin",
                url: "Thay-doi-thong-tin",
                defaults: new { controller = "DangNhapDangKy", action = "ThayDoiThongTin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Thong-tin-san-pham",
                url: "Thong-tin-san-pham-{id}",
                defaults: new { controller = "DanhSachSanPhams", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Danh-sach-hoa-don",
                url: "Danh-sach-hoa-don",
                defaults: new { controller = "MuaHangs", action = "XemHoaDon", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Chi-tiet-hoa-don",
                url: "Chi-tiet-hoa-don-{id}",
                defaults: new { controller = "ChiTietHoaDons", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Chi-tiet-san-pham",
                url: "Chi-tiet-san-pham-{id}",
                defaults: new { controller = "DanhSachSanPhams", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Trang-khuyen-mai",
                url: "Trang-khuyen-mai",
                defaults: new { controller = "TrangKhuyenMai", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Trang-lien-he",
                url: "Trang-lien-he",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Trang-gioi-thieu",
                url: "Trang-gioi-thieu",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

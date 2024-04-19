using PBL3.Models.Model_View;
using PBL3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PBL3.Controllers
{
    public class GioHangsController : Controller
    {
        private readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        public GioHangs LayGioHang()
        {

            GioHangs giohang = Session["GioHang"] as GioHangs;
            if (giohang == null || Session["GioHang"] == null)
            {
                giohang = new GioHangs();
                Session["GioHang"] = giohang;
                Session["SoLuong"] = 0;
            }
            else Session["SoLuong"] = giohang.Dem();
            return giohang;


        }
        // Thêm vào giỏ hàng
        public ActionResult ThemVaoGioHang(int id)
        {
            var sanphams = db.SanPhams.FirstOrDefault(x => x.ID_SP == id);
            if (sanphams != null)
            {
                int soluong = 0;
                if (sanphams.SoLuong != 0)
                {
                    LayGioHang().ThemVaoGio(sanphams);
                    soluong = 1;
                }
                else LayGioHang().ThemVaoGio(sanphams, soluong);
                if (Session["ID_Account"].Equals("") == false)
                {
                    int ma = Convert.ToInt32(Session["ID_Account"]);
                    var themmoi = db.GioHangs.FirstOrDefault(x => x.ID_SP == id && x.ID_GioHang == ma);
                    if (themmoi == null)
                    {
                        db.GioHangs.Add(new GioHang
                        {
                            ID_GioHang = ma,
                            ID_SP = id,
                            SoLuong = soluong

                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        GioHang newgh = db.GioHangs.FirstOrDefault(x => x.ID_GioHang == ma && x.ID_SP == id);
                        newgh.SoLuong += soluong;
                        db.Entry(newgh).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                Session["SoLuong"] = LayGioHang().Dem();

            }
            var url = Request.UrlReferrer;
            if (url != null)
            {
                return Redirect(url.ToString());
            }
            else
            {
                return RedirectToAction("Index", "DanhSachSanPhams");
            }
        }
        // Xem giỏ hàng
        public ActionResult XemGioHang()
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "DanhSachSanPhams");
            GioHangs giohang = Session["GioHang"] as GioHangs;
            Session["SoLuong"] = giohang.Dem();
            return View(giohang);
        }
        [HttpPost]
        public ActionResult ThayDoi(FormCollection form)
        {
            try
            {
                GioHangs giohang = Session["GioHang"] as GioHangs;
                
                if (form.Count != 0)
                {
                    var listid = form["idSanPham"];
                    var id = listid.Split(',');
                    var listsoluong = form["SoLuongSanPham"];
                    var soluong = listsoluong.Split(',');
                    var liststatus = form["status"];
                    var status = liststatus.Split(',');
                    for (int x = 0; x < id.Length; x++)
                    {

                        giohang.ThayDoiSoLuong(int.Parse(id[x]), int.Parse(soluong[x]), int.Parse(status[x]));
                        Session["SoLuong"] = giohang.Dem();
                        if (Session["ID_Account"].Equals("") == false)
                        {
                            int ma = Convert.ToInt32(Session["ID_Account"]);
                            int masp = int.Parse(id[x]);
                            GioHang newgh = db.GioHangs.FirstOrDefault(y => y.ID_GioHang == ma && y.ID_SP == masp);
                            if (newgh != null)
                            {
                                newgh.SoLuong = int.Parse(soluong[x]);
                                db.Entry(newgh).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                newgh = new GioHang();
                                newgh.ID_GioHang = ma;
                                newgh.ID_SP = masp;
                                newgh.SoLuong = int.Parse(soluong[x]);
                                db.GioHangs.Add(newgh);
                                db.SaveChanges();
                            }
                        }
                    } 
                }

            }
            catch
            {
                Response.Redirect("~/Trang-chu");
            }
            return RedirectToAction("XemGioHang", "GioHangs");
        }
        public ActionResult XoaSanPham(int id)
        {
            GioHangs giohang = Session["GioHang"] as GioHangs;

            giohang.Xoa(id);
            Session["SoLuong"] = giohang.Dem();
            if (giohang.Items.Count() == 0)
                Session["GioHang"] = null;
            if (Session["ID_Account"].Equals("") == false)
            {
                
                int ma = Convert.ToInt32(Session["ID_Account"]);
                GioHang newgh = db.GioHangs.FirstOrDefault(x => x.ID_GioHang == ma && x.ID_SP == id);
                db.GioHangs.Remove(newgh);
                db.SaveChanges();
            }
            return RedirectToAction("XemGioHang", "GioHangs");
        }
        public ActionResult HuyGioHang()
        {
            GioHangs giohang = Session["GioHang"] as GioHangs;
            
            giohang.Huy();
            Session["SoLuong"] = 0;
            Session["GioHang"] = null;
            if (Session["ID_Account"].Equals("") == false)
            {

                int ma = Convert.ToInt32(Session["ID_Account"]);
                var listhanghoa = db.GioHangs.Where(x => x.ID_GioHang == ma);
                foreach (var hanghoa in listhanghoa)
                {
                    db.GioHangs.Remove(hanghoa);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "DanhSachSanPhams");
        }
        
    }

}

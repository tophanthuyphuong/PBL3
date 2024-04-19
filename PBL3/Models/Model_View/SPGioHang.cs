using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PBL3.Models;

namespace PBL3.Models.Model_View
{
    public class SPGioHang
    {
        public SanPham SanPhamGioHang { get; set; }
        public int SoLuong { get; set; }
        public bool Status { get; set; }
        public int NoiDungKhuyenMai { get; set; }
    }
    // Giỏ hàng
    public class GioHangs
    {

        List<SPGioHang> items = new List<SPGioHang>();
        public IEnumerable<SPGioHang> Items
        {
            get { return items; }

        }
        readonly CuaHangDienMayEntities db = new CuaHangDienMayEntities();
        public void ThemVaoGio(SanPham SP, int soluong = 1)
        {
            var item = items.FirstOrDefault(x => x.SanPhamGioHang.ID_SP == SP.ID_SP);
            if (item == null)
            {
                int kms = 0;
                var km = db.KhuyenMais.FirstOrDefault(x => x.ID_KM == SP.ID_KM);
                if (km != null)
                {
                    kms = Convert.ToInt32(km.NoiDungKM);
                }
                if (soluong != 0)
                {
                    items.Add(new SPGioHang
                    {
                        SanPhamGioHang = SP,
                        SoLuong = soluong,
                        Status = true,
                        NoiDungKhuyenMai = kms
                    }); 
                }
                else
                {
                    items.Add(new SPGioHang
                    {
                        SanPhamGioHang = SP,
                        SoLuong = soluong,
                        Status = false,
                        NoiDungKhuyenMai = kms
                    });
                }

            }
            else
            {
                if (item.SanPhamGioHang.SoLuong > item.SoLuong + soluong)
                    item.SoLuong += soluong;
                else item.SoLuong = (int)item.SanPhamGioHang.SoLuong;
                if (soluong == 0) item.SoLuong = 0;
            }
        }
        public void ThayDoiSoLuong(int id, int soluong, int check = 1)
        {
            var item = items.Find(x => x.SanPhamGioHang.ID_SP == id);
            if (item != null)
            {
                item.SoLuong = soluong;
                if(soluong != 0)
                    item.Status = Convert.ToBoolean(check);
                else
                    item.Status = false;
            }

        }

        public void Xoa(int id)
        {
            items.RemoveAll(x => x.SanPhamGioHang.ID_SP == id);
        }
        public void Huy()
        {
            items.Clear();
        }
        public void XoaSanPhamDaMua()
        {
            items.RemoveAll(x => x.Status == true);
        }
        public int Dem()
        {
            int total = 0;
            foreach (SPGioHang item in items)
            {
                total += 1;
            }
            return (int)total;
        }

    }
}
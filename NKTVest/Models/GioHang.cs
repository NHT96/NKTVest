using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NKTVest.Models
{
    public class GioHang
    {
        NKTVDataContext data = new NKTVDataContext();
        public string gMasp { get; set; }
        public string gTensp { get; set; }
        public string gAnhbia { set; get; }
        public int gDongia { get; set; }
        public int gSoluong { get; set; }
        public int gTongtien { get { return gSoluong * gDongia; } }

        public GioHang(string MASP)
        {
            gMasp = MASP;
            SANPHAM sanpham = data.SANPHAMs.Single(n => n.MASP == gMasp);
            gTensp = sanpham.TENSP;
            gAnhbia = sanpham.ANHBIA;
            gDongia = int.Parse(sanpham.DONGIA.ToString());
            gSoluong = 1;

        }
    }
}
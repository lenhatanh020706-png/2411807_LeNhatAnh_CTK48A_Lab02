using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienKhoaCNTT
{
    public class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; } // true: Nam, false: Nữ
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public string SoDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Hinh { get; set; }

        public SinhVien()
        {
            NgaySinh = DateTime.Now;
            GioiTinh = true;
        }

        public SinhVien(string mssv, string hoTen, bool gioiTinh, DateTime ngaySinh, string lop, string soDT, string email, string diaChi, string hinh)
        {
            MSSV = mssv;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            Lop = lop;
            SoDT = soDT;
            Email = email;
            DiaChi = diaChi;
            Hinh = hinh;
        }

        public override string ToString()
        {
            return $"{MSSV}\t{HoTen}\t{(GioiTinh ? "1" : "0")}\t{NgaySinh:dd/MM/yyyy}\t{Lop}\t{SoDT}\t{Email}\t{DiaChi}\t{Hinh}";
        }
    }
}

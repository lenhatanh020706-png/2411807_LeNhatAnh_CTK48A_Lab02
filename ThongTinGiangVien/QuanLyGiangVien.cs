using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ThongTinGiangVien.Form1;
using static ThongTinGiangVien.Form1.DanhMucHocPhan;

namespace ThongTinGiangVien
{
    public enum KieuTim
    {
        TheoMa,
        TheoHoTen,
        TheoSDT
    }

    public delegate int SoSanh(object a, object b);

  
    public class QuanLyGiangVien
    {
        public List<GiangVien> dsGiangVien { get; set; }

        public QuanLyGiangVien()
        {
            dsGiangVien = new List<GiangVien>();
        }

        public GiangVien this[int index]
        {
            get
            {
                if (index >= 0 && index < dsGiangVien.Count)
                    return dsGiangVien[index];
                return null;
            }
            set
            {
                if (index >= 0 && index < dsGiangVien.Count)
                    dsGiangVien[index] = value;
            }
        }

        public bool Them(GiangVien gv)
        {
            if (gv == null || string.IsNullOrWhiteSpace(gv.MaSo))
                return false;

            // Mỗi giảng viên chỉ có 1 mã duy nhất
            bool exists = dsGiangVien.Any(item => string.Equals(item.MaSo, gv.MaSo, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                return false;
            }

            dsGiangVien.Add(gv);
            return true;
        }

        public GiangVien Tim(object temp, SoSanh ss)
        {
            foreach (var gv in dsGiangVien)
            {
                if (ss(temp, gv) == 0)
                {
                    return gv;
                }
            }
            return null;
        }

        public bool Xoa(object temp, SoSanh ss)
        {
            for (int i = dsGiangVien.Count - 1; i >= 0; i--)
            {
                if (ss(temp, dsGiangVien[i]) == 0)
                {
                    dsGiangVien.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void SapXep(SoSanh ss)
        {
            dsGiangVien.Sort((x, y) => ss(x, y));
        }

    }
}

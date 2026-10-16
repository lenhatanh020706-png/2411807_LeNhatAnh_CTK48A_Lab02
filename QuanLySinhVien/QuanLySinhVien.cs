    using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVien
{

    public delegate int SoSanh(object sv1, object sv2);

    public class QuanLySinhVien
    {
        public List<SinhVien> dsSinhVien;

        public QuanLySinhVien()
        {
            dsSinhVien = new List<SinhVien>();
        }

        public SinhVien this[int index]
        {
            get { return this.dsSinhVien[index]; }
            set { dsSinhVien[index] = value; }
        }

        public void Them(SinhVien sv)
        {
            this.dsSinhVien.Add(sv);
        }

        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien svresult = null;
            foreach (SinhVien sv in dsSinhVien)
            {
                if (ss(obj, sv) == 0)
                {
                    svresult = sv;
                    break;
                }
            }
            return svresult;
        }

        public bool Sua(SinhVien svsua, object obj, SoSanh ss)
        {
            int i, count;
            bool kq = false;
            count = this.dsSinhVien.Count;
            for (i = 0; i < count; i++)
            {
                if (ss(obj, this[i]) == 0)
                {
                    this[i] = svsua;
                    kq = true;
                    break;
                }
            }
            return kq;
        }

        public void Xoa(object obj, SoSanh ss)
        {
            int i = dsSinhVien.Count - 1;
            for (; i >= 0; i--)
            {
                if (ss(obj, this[i]) == 0)
                    this.dsSinhVien.RemoveAt(i);
            }
        }

        public void DocTuFile(string filename)
        {
            if (!File.Exists(filename))
                return;

            string t;
            string[] s;
            SinhVien sv;
            using (StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                while ((t = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(t))
                        continue;

                    s = t.Split('\t');
                    if (s.Length < 8)
                        continue;

                    sv = new SinhVien();
                    sv.MaSo = s[0];
                    sv.HoTen = s[1];

                    DateTime dt;
                    if (DateTime.TryParseExact(s[2].Trim(),
                        new string[] { "M/d/yyyy", "MM/dd/yyyy", "d/M/yyyy", "dd/MM/yyyy" },
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dt))
                    {
                        sv.NgaySinh = dt;
                    }
                    else if (DateTime.TryParse(s[2].Trim(), out dt))
                    {
                        sv.NgaySinh = dt;
                    }
                    else
                    {
                        sv.NgaySinh = DateTime.Now;
                    }

                    sv.DiaChi = s[3];
                    sv.Lop = s[4];
                    sv.Hinh = s[5];
                    sv.GioiTinh = false;
                    if (s[6] == "1")
                        sv.GioiTinh = true;

                    string[] cn = s[7].Split(',');
                    foreach (string c in cn)
                    {
                        if (!string.IsNullOrWhiteSpace(c))
                            sv.ChuyenNganh.Add(c.Trim());
                    }

                    this.Them(sv);
                }
            }
        }
    }
    }

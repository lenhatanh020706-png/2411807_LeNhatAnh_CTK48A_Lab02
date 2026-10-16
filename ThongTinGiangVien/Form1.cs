using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ThongTinGiangVien.Form1;
using static ThongTinGiangVien.Form1.DanhMucHocPhan;

namespace ThongTinGiangVien
{
    public partial class Form1 : Form
    {
        private QuanLyGiangVien qlgv;
        public Form1()
        {
            InitializeComponent();
            qlgv = new QuanLyGiangVien();

            SeedSampleGiangVien();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string lienHe = "https://cntt.dlu.edu.vn/";
            this.linkLabel1.Links.Add(0, lienHe.Length, lienHe);
            this.cboMaSo.SelectedItem = this.cboMaSo.Items[0];
        }
        public class HocPhan
        {
            // Các thuộc tính (Properties)
            public int ID { get; set; }
            public string TenHP { get; set; }
            public int SoTC { get; set; }

            // Phương thức khởi tạo 1 tham số (Constructor)
            public HocPhan(string ten)
            {
                this.TenHP = ten;
            }

            // Phương thức khởi tạo đầy đủ tham số (Constructor)
            public HocPhan(int id, string ten, int tc)
            {
                this.ID = id;
                this.TenHP = ten;
                this.SoTC = tc;
            }

            // Ghi đè phương thức hiển thị chuỗi (Override ToString)
            public override string ToString()
            {
                return TenHP;
            }
        }
        public class DanhMucHocPhan
        {
            // Nên chuyển thành private hoặc sử dụng Property để đảm bảo tính đóng gói
            public List<HocPhan> ds;

            // Hàm khởi tạo (Constructor)
            public DanhMucHocPhan()
            {
                ds = new List<HocPhan>();
            }

            // Indexer để truy cập phần tử qua chỉ số (vd: danhMuc[0])
            public HocPhan this[int index]
            {
                get { return ds[index]; } // Sửa lỗi: Không cần dùng 'as HocPhan'
                set { ds[index] = value; }
            }

            // Phương thức thêm 1 học phần vào danh sách
            public void Them(HocPhan hp)
            {
                ds.Add(hp);
            }

            // Phương thức ghi đè ToString để xuất chuỗi
            public override string ToString()
            {
                string s = "Danh sach mon hoc: ";
                foreach (HocPhan mh in ds) // Sửa lỗi: Duyệt trực tiếp bằng kiểu HocPhan
                {
                    s += mh.ToString() + "; "; // Sửa lỗi: Gọi phương thức ToString() của học phần
                }
                return s;
            }

            public class GiangVien
            {
                // Các thuộc tính và thuộc tính tự động (Properties)
                public string MaSo { get; set; }
                public string HoTen { get; set; }
                public DateTime NgaySinh { get; set; }

                // Các biến thành viên (Fields)
                public DanhMucHocPhan dsHocPhan;
                public string GioiTinh;
                public string[] NgoaiNgu;
                public string SoDT;
                public string Mail;

                // Constructor không tham số (Mặc định)
                public GiangVien()
                {
                    dsHocPhan = new DanhMucHocPhan();
                    NgoaiNgu = new string[20];
                }

                // Constructor có tham số
                public GiangVien(string maso, string sdt, string mail, string hoten,
                                 DateTime ngaysinh, DanhMucHocPhan ds, string gt, string[] nn)
                {
                    this.MaSo = maso;
                    this.HoTen = hoten;
                    this.NgaySinh = ngaysinh;
                    this.dsHocPhan = ds;
                    this.NgoaiNgu = nn;
                    this.SoDT = sdt;
                    this.Mail = mail;
                    this.GioiTinh = gt;
                }

                // Ghi đè phương thức hiển thị thông tin
                public override string ToString()
                {
                    string s = "Mã số: " + MaSo + "\n"
                             + "Họ tên: " + HoTen + "\n"
                             + "Ngày sinh: " + NgaySinh + "\n"
                             + "Giới tính: " + GioiTinh + "\n"
                             + "Số ĐT: " + SoDT + "\n"
                             + "Mail: " + Mail + "\n";

                    // Chuỗi danh sách ngoại ngữ
                    string sngoaingu = "Ngoại ngữ: ";
                    foreach (string t in NgoaiNgu)
                    {
                        sngoaingu += t + "; ";
                    }

                    // Chuỗi danh sách môn dạy
                    string monDay = "Danh sách môn dạy: ";
                    foreach (HocPhan hp in dsHocPhan.ds)
                    {
                        monDay += hp + "; ";
                    }

                    s += "\n" + sngoaingu + "\n" + monDay;
                    return s;
                }
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strlink = e.Link.LinkData.ToString();
            try
            {
                // Fix for .NET Core / .NET 5+ where UseShellExecute defaults to false
                Process.Start(new ProcessStartInfo
                {
                    FileName = strlink,
                    UseShellExecute = true
                });
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Alternative fallback for specific environments (like Linux/macOS if needed)
                // Or to catch invalid link paths gracefully
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            // Xóa nội dung các ô nhập liệu
            this.cboMaSo.Text = "";
            this.txtHoTen.Text = "";
            this.txtMail.Text = "";
            this.mtxtSoDT.Text = "";

            // Reset giới tính về Nam
            this.rdNam.Checked = true;

            // Bỏ chọn tất cả ngoại ngữ
            for (int i = 0; i < chklbNgoaiNgu.Items.Count; i++)
            {
                chklbNgoaiNgu.SetItemChecked(i, false);
            }

            // Chuyển các học phần đang dạy về danh sách học phần
            if (this.lbHocPhanDay.Items.Count > 0)
            {
                object[] itemsToMove = new object[this.lbHocPhanDay.Items.Count];

                this.lbHocPhanDay.Items.CopyTo(itemsToMove, 0);
                this.lbDanhSachHP.Items.AddRange(itemsToMove);

                // Xóa danh sách học phần đang dạy
                this.lbHocPhanDay.Items.Clear();
            }
        }
        public GiangVien GetGiangVien()
        {
            GiangVien gv = new GiangVien();

            // 1. Lấy giới tính
            gv.GioiTinh = rdNu.Checked ? "Nữ" : "Nam";

            // 2. Gán các thông tin cơ bản
            gv.MaSo = this.cboMaSo.Text;
            gv.HoTen = this.txtHoTen.Text;
            gv.NgaySinh = this.dtpNgaySinh.Value;
            gv.Mail = this.txtMail.Text;
            gv.SoDT = this.mtxtSoDT.Text;

            // 3. Xử lý ngoại ngữ (Sử dụng CheckedItems để tránh sót và lặp thừa)
            List<string> listNgoaiNgu = new List<string>();
            foreach (var item in chklbNgoaiNgu.CheckedItems)
            {
                listNgoaiNgu.Add(item.ToString());
            }
            gv.NgoaiNgu = listNgoaiNgu.ToArray();

            // 4. Xử lý danh sách học phần
            DanhMucHocPhan dshp = new DanhMucHocPhan();
            foreach (object hp in lbHocPhanDay.Items)
            {
                dshp.Them(new HocPhan(hp.ToString()));
            }
            gv.dsHocPhan = dshp;

            return gv;
        }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
            TBGiangVien frm = new TBGiangVien();
            // Lấy thông tin giảng viên và hiển thị lên form thông báo
            frm.SetText(GetGiangVien().ToString());
            frm.ShowDialog();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            int i = this.lbDanhSachHP.SelectedItems.Count - 1;
            while (i >= 0)
            {
                this.lbHocPhanDay.Items.Add(lbDanhSachHP.SelectedItems[i]);
                this.lbDanhSachHP.Items.Remove(lbDanhSachHP.SelectedItems[i]);
                i--;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int i = this.lbHocPhanDay.SelectedItems.Count - 1;
            while (i >= 0)
            {
                this.lbDanhSachHP.Items.Add(lbHocPhanDay.SelectedItems[i]);
                this.lbHocPhanDay.Items.Remove(lbHocPhanDay.SelectedItems[i]);
                i--;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            using (frmTimGiangVien frm = new frmTimGiangVien(this.qlgv))
            {
                frm.ShowDialog();
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(this.cboMaSo.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã số giảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cboMaSo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên giảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtHoTen.Focus();
                return false;
            }

            return true;
        }
        
        private void SeedSampleGiangVien()
        {
            var gv1 = new GiangVien
            {
                MaSo = "001",
                GioiTinh = "Nam",
                HoTen = "Nguyễn Văn An",
                NgaySinh = new DateTime(1985, 5, 15),
                Mail = "an.nguyen@cntt.edu.vn",
                SoDT = "(0901).234.567",
                NgoaiNgu = new string[] { "Tiếng Anh" },
                dsHocPhan = new DanhMucHocPhan()
            };
            gv1.dsHocPhan.Them(new HocPhan("Tin học cơ sở"));
            gv1.dsHocPhan.Them(new HocPhan("Lập trình hướng đối tượng"));
            qlgv.Them(gv1);

            var gv2 = new GiangVien
            {
                MaSo = "002",
                GioiTinh = "Nữ",
                HoTen = "Nguyen Thi C",
                NgaySinh = new DateTime(1999, 9, 23, 16, 59, 39),
                Mail = "cc@yahoo.com",
                SoDT = "(0633).333.333",
                NgoaiNgu = new string[] { "Tiếng Anh" },
                dsHocPhan = new DanhMucHocPhan()
            };
            gv2.dsHocPhan.Them(new HocPhan("Phân tích TKHDT"));
            gv2.dsHocPhan.Them(new HocPhan("Lập trình cấu trúc C/C++"));
            qlgv.Them(gv2);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {

            if (!ValidateInput())
                return;

            GiangVien gv = GetGiangVien();
            bool kq = qlgv.Them(gv);
            if (!kq)
            {
                MessageBox.Show("Mã giảng viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Thêm giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}

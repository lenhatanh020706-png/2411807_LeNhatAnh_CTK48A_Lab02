using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ThongTinGiangVien.Form1;
using static ThongTinGiangVien.Form1.DanhMucHocPhan;

namespace ThongTinGiangVien
{
    public partial class frmTimGiangVien : Form
    {
        public frmTimGiangVien()
        {
            InitializeComponent();
        }

        private void frmTimGiangVien_Load(object sender, EventArgs e)
        {

        }

        private QuanLyGiangVien qlgv;
       
        public frmTimGiangVien(QuanLyGiangVien qlgv) : this()
        {
            this.qlgv = qlgv;
        }

        private void rdTim_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMaGV.Checked)
            {
                lblTim.Text = "Mã GV";
            }
            else if (rdHoTen.Checked)
            {
                lblTim.Text = "Họ tên";
            }
            else if (rdSoDT.Checked)
            {
                lblTim.Text = "Số điện thoại";
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = txtTim.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTim.Focus();
                return;
            }

            if (qlgv == null || qlgv.dsGiangVien == null || qlgv.dsGiangVien.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GiangVien gv = null;
            if (rdMaGV.Checked)
            {
                gv = qlgv.Tim(query, (temp, item) =>
                {
                    GiangVien g = item as GiangVien;
                    if (g == null) return -1;
                    return string.Compare(g.MaSo.Trim(), temp.ToString().Trim(), StringComparison.OrdinalIgnoreCase);
                });
            }
            else if (rdHoTen.Checked)
            {
                gv = qlgv.Tim(query, (temp, item) =>
                {
                    GiangVien g = item as GiangVien;
                    if (g == null) return -1;
                    string q = temp.ToString().Trim();
                    return g.HoTen.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : 1;
                });
            }
            else if (rdSoDT.Checked)
            {
                gv = qlgv.Tim(query, (temp, item) =>
                {
                    GiangVien g = item as GiangVien;
                    if (g == null) return -1;
                    string qDigits = new string(temp.ToString().Where(char.IsDigit).ToArray());
                    string gvDigits = new string(g.SoDT.Where(char.IsDigit).ToArray());
                    if (!string.IsNullOrEmpty(qDigits) && !string.IsNullOrEmpty(gvDigits))
                    {
                        return gvDigits.Contains(qDigits) ? 0 : 1;
                    }
                    return g.SoDT.IndexOf(temp.ToString().Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : 1;
                });
            }

            if (gv != null)
            {
                TBGiangVien frm = new TBGiangVien();
                frm.SetText(gv.ToString());
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTim_Click_1(object sender, EventArgs e)
        {
            string query = txtTim.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTim.Focus();
                return;
            }

            if (qlgv == null || qlgv.dsGiangVien == null || qlgv.dsGiangVien.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GiangVien gv = null;
            if (rdMaGV.Checked)
            {
                gv = qlgv.Tim(query, (temp, item) =>
                {
                    GiangVien g = item as GiangVien;
                    if (g == null) return -1;
                    return string.Compare(g.MaSo.Trim(), temp.ToString().Trim(), StringComparison.OrdinalIgnoreCase);
                });
            }
            else if (rdHoTen.Checked)
            {
                gv = qlgv.Tim(query, (temp, item) =>
                {
                    GiangVien g = item as GiangVien;
                    if (g == null) return -1;
                    string q = temp.ToString().Trim();
                    return g.HoTen.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : 1;
                });
            }
            else if (rdSoDT.Checked)
            {
                gv = qlgv.Tim(query, (temp, item) =>
                {
                    GiangVien g = item as GiangVien;
                    if (g == null) return -1;
                    string qDigits = new string(temp.ToString().Where(char.IsDigit).ToArray());
                    string gvDigits = new string(g.SoDT.Where(char.IsDigit).ToArray());
                    if (!string.IsNullOrEmpty(qDigits) && !string.IsNullOrEmpty(gvDigits))
                    {
                        return gvDigits.Contains(qDigits) ? 0 : 1;
                    }
                    return g.SoDT.IndexOf(temp.ToString().Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : 1;
                });
            }

            if (gv != null)
            {
                TBGiangVien frm = new TBGiangVien();
                frm.SetText(gv.ToString());
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

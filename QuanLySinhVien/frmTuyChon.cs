using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class frmTuyChon : Form
    {
        public frmTuyChon()
        {
            InitializeComponent();
        }
        public TuyChon Kieu { get; set; }
        public string ChuoiTim { get; set; }

        private bool isTimKiem;
        private QuanLySinhVien qlsv;
        private ListView lvSinhVien;
        private Action reloadListView;
        public frmTuyChon(bool isTimKiem, QuanLySinhVien qlsv, ListView lvSinhVien, Action reloadListView) : this()
        {
            this.isTimKiem = isTimKiem;
            this.qlsv = qlsv;
            this.lvSinhVien = lvSinhVien;
            this.reloadListView = reloadListView;
        }
        private void btnSapXep_Click(object sender, EventArgs e)
        {
            if (qlsv == null || qlsv.dsSinhVien == null)
                return;

            if (rdMaSV.Checked)
            {
                qlsv.dsSinhVien.Sort((a, b) => string.Compare(a.MaSo, b.MaSo, StringComparison.OrdinalIgnoreCase));
            }
            else if (rdHoTen.Checked)
            {
                qlsv.dsSinhVien.Sort((a, b) => string.Compare(a.HoTen, b.HoTen, StringComparison.OrdinalIgnoreCase));
            }
            else if (rdNgaySinh.Checked)
            {
                qlsv.dsSinhVien.Sort((a, b) => DateTime.Compare(a.NgaySinh, b.NgaySinh));
            }

            reloadListView?.Invoke();
            this.Close();
        }

        private void frmTuyChon_Load(object sender, EventArgs e)
        {

        }

        private void rdHoTen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdMaSV_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdNgaySinh_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = txtThongTin.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Hãy nhập thông tin tìm!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtThongTin.Focus();
                return;
            }

            ChuoiTim = query;
            int count = 0;

            if (lvSinhVien != null)
            {
                lvSinhVien.SelectedItems.Clear();
                foreach (ListViewItem item in lvSinhVien.Items)
                {
                    bool match = false;
                    if (rdMaSV.Checked)
                    {
                        match = item.SubItems[0].Text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    else if (rdHoTen.Checked)
                    {
                        match = item.SubItems[1].Text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    else if (rdNgaySinh.Checked)
                    {
                        match = item.SubItems[2].Text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (!match && int.TryParse(query, out int num))
                        {
                            if (DateTime.TryParse(item.SubItems[2].Text, out DateTime dt))
                            {
                                match = (dt.Day == num || dt.Month == num || dt.Year == num);
                            }
                        }
                    }

                    if (match)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        count++;
                    }
                }
            }

            MessageBox.Show("Số sinh viên tìm Thấy:" + count, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBoxTim_Enter(object sender, EventArgs e)
        {

        }
        private void rdTuyChon_Click(object sender, EventArgs e)
        {
            if (rdMaSV.Checked)
                Kieu = TuyChon.MaSV;
            else if (rdHoTen.Checked)
                Kieu = TuyChon.HoTen;
            else if (rdNgaySinh.Checked)
                Kieu = TuyChon.NgaySinh;
        }
    }
}

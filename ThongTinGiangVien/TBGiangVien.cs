using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThongTinGiangVien
{
    public partial class TBGiangVien : Form
    {
        public TBGiangVien()
        {
            InitializeComponent();
        }

        private void TBGiangVien_Load(object sender, EventArgs e)
        {

        }
        public void SetText(string s)
        {
            this.lblThongBao.Text = s;
        }

    }
}

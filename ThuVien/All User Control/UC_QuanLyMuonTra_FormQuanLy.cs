using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThuVien.All_User_Control
{
    public partial class UC_QuanLyMuonTra_FormQuanLy : UserControl
    {
        public UC_QuanLyMuonTra_FormQuanLy()
        {
            InitializeComponent();
        }

        private void UC_QuanLyMuonTra_FormQuanLy_Load(object sender, EventArgs e)
        {
            btnTatCa.Checked = true;
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThuVien.All_User_Control;

namespace ThuVien
{
    public partial class DB_quan_li : Form
    {
        public DB_quan_li()
        {
            InitializeComponent();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            uC_TongQuanQuanLy1.Visible = true;
            uC_TongQuanQuanLy1.BringToFront();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            uC_KhoSach_FormQuanLy1.Visible = true;
            uC_KhoSach_FormQuanLy1.BringToFront();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            uC_TroLyAI_FormQuanLy1.Visible = true;
            uC_TroLyAI_FormQuanLy1.BringToFront();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            uC_QuanLyMuonTra_FormQuanLy1.Visible = true;
            uC_QuanLyMuonTra_FormQuanLy1.BringToFront();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            uC_NhanSu_FormQuanLy1.Visible = true;
            uC_NhanSu_FormQuanLy1.BringToFront();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            uC_CaiDat1.Visible = true;
            uC_CaiDat1.BringToFront();
        }

        private void uC_TongQuanQuanLy1_Load(object sender, EventArgs e)
        {
            btnTongQuan.Checked= true;
        }
    }
}

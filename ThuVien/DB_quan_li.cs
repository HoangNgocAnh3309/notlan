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

        private void DB_quan_li_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            uC_TongQuanQuanLy2.Visible = true;
            uC_TongQuanQuanLy2.BringToFront();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            uC_KhoSach_FormQuanLy2.Visible=true;
            uC_KhoSach_FormQuanLy2.BringToFront();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            uC_TroLyAI_FormQuanLy2.Visible=true;
            uC_TroLyAI_FormQuanLy2.BringToFront();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            uC_QuanLyMuonTra_FormQuanLy2.Visible=true;
            uC_QuanLyMuonTra_FormQuanLy2.BringToFront();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            uC_NhanSu_FormQuanLy2.Visible=true;
            uC_NhanSu_FormQuanLy2.BringToFront();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            uC_CaiDat2.Visible = true;
            uC_CaiDat2.BringToFront();
        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

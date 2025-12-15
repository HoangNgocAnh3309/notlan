using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThuVien.All_User_Control
{
    public partial class UC_Quan_li_dang_nhap : UserControl
    {
        public UC_Quan_li_dang_nhap()
        {
            InitializeComponent();
     
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;

        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            if (txt_password.Text == "123")
            {
                DB_quan_li db = new DB_quan_li();
                db.Show();

                UC_TongQuanQuanLy uc = new UC_TongQuanQuanLy();
                uc.Dock = DockStyle.Fill;

                db.guna2Panel5.Controls.Clear();
                db.guna2Panel5.Controls.Add(uc);
            }
        }



    }
}

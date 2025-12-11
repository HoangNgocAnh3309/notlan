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
    public partial class UC_doc_gia_dang_nhap : UserControl
    {
        public UC_doc_gia_dang_nhap()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;

        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            if(txt_password.Text == "123")
            {
                DB_doc_gia db =new DB_doc_gia();
                db.Show();
            }
        }
    }
}

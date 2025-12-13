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
    public partial class UC_dang_nhap_thu_thu : UserControl
    {
        public UC_dang_nhap_thu_thu()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if(guna2TextBox2.Text == "123")
            {
                DB_thu_thu db = new DB_thu_thu();
                db.Show();
            }
        }
    }
}

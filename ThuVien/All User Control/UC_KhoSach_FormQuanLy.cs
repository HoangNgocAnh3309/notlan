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
    public partial class UC_KhoSach_FormQuanLy : UserControl
    {
        public UC_KhoSach_FormQuanLy()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
           
        
        }

        private void btnThemSach_Click_1(object sender, EventArgs e)
        {
            var uc = guna2Panel3.Controls
                .OfType<UC_them_sach>()
                .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_them_sach();
                uc.Dock = DockStyle.Fill;
                guna2Panel3.Controls.Add(uc);
            }
            uc.Show();
            uc.BringToFront();
        }

    }
}

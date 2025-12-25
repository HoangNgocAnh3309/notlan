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
            var uc = guna2Panel5.Controls
                      .OfType<UC_TongQuanQuanLy>()
                      .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_TongQuanQuanLy();
                uc.Dock = DockStyle.Fill;
                guna2Panel5.Controls.Add(uc);
            }

            uc.BringToFront();
        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var uc = guna2Panel5.Controls
                      .OfType<UC_KhoSach_FormQuanLy>()
                      .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_KhoSach_FormQuanLy();
                uc.Dock = DockStyle.Fill;
                guna2Panel5.Controls.Add(uc);
            }

            uc.BringToFront();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            var uc = guna2Panel5.Controls
                      .OfType<UC_TroLyAI_FormQuanLy>()
                      .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_TroLyAI_FormQuanLy();
                uc.Dock = DockStyle.Fill;
                guna2Panel5.Controls.Add(uc);
            }

            uc.BringToFront();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            var uc = guna2Panel5.Controls
                      .OfType<UC_QuanLyMuonTra_FormQuanLy>()
                      .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_QuanLyMuonTra_FormQuanLy();
                uc.Dock = DockStyle.Fill;
                guna2Panel5.Controls.Add(uc);
            }

            uc.BringToFront();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var uc = guna2Panel5.Controls
                      .OfType<UC_NhanVien_FromQuanLy>()
                      .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_NhanVien_FromQuanLy();
                uc.Dock = DockStyle.Fill;
                guna2Panel5.Controls.Add(uc);
            }

            uc.BringToFront();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var uc = guna2Panel5.Controls
                      .OfType<UC_CaiDat>()
                      .FirstOrDefault();

            if (uc == null)
            {
                uc = new UC_CaiDat();
                uc.Dock = DockStyle.Fill;
                guna2Panel5.Controls.Add(uc);
            }

            uc.BringToFront();
        }


        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

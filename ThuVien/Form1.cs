using Guna.UI2.WinForms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = "\"Thư viện không chỉ là nơi giữ sách, đó là\nnơi giữ gìn tri thức và tâm hồn của nhân\nloại.\"";
            guna2Button7.MouseEnter += guna2Button7_MouseEnter;
            guna2Button7.MouseLeave += guna2Button7_MouseLeave;
            guna2Button4.MouseEnter += guna2Button4_MouseEnter;
            guna2Button4.MouseLeave += guna2Button4_MouseLeave;
            label7.MouseEnter += label7_MouseEnter;
            label7.MouseLeave += label7_MouseLeave;
            guna2Button5.MouseEnter += guna2Button5_MouseEnter;
            guna2Button5.MouseLeave += guna2Button5_MouseLeave;
            guna2Button8.MouseEnter += guna2Button8_MouseEnter;
            guna2Button8.MouseLeave += guna2Button8_MouseLeave;
            label8.MouseEnter += label8_MouseEnter;
            label8.MouseLeave += label8_MouseLeave;
            guna2Button6.MouseEnter += guna2Button6_MouseEnter;
            guna2Button6.MouseLeave += guna2Button6_MouseLeave;
            guna2Button9.MouseEnter += guna2Button9_MouseEnter;
            guna2Button9.MouseLeave += guna2Button9_MouseLeave;
            label9.MouseEnter += label9_MouseEnter;
            label9.MouseLeave += label9_MouseLeave;
            label6.MouseEnter += label6_MouseEnter;
            label6.MouseLeave += label6_MouseLeave;


        }
        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Underline);

        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Font = new Font(label6.Font, FontStyle.Regular);
        }
        private void guna2Button7_MouseEnter(object sender, EventArgs e)
        {
            guna2Button4.FillColor = Color.White;   // Màu khi hover
            guna2Button4.ForeColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button4.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button4.Image = Properties.Resources.arrow_right__1_;

        }

        private void guna2Button7_MouseLeave(object sender, EventArgs e)
        {
            guna2Button4.FillColor = Color.White;  // Màu ban đầu
            guna2Button4.ForeColor = Color.Black;
            guna2Button4.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
            guna2Button4.Image = null;
        }
        private void guna2Button4_MouseEnter(object sender, EventArgs e)
        {
            guna2Button7.FillColor = ColorTranslator.FromHtml("#2563EB");
            guna2Button7.Image = Properties.Resources.user__1_;


        }

        private void guna2Button4_MouseLeave(object sender, EventArgs e)
        {
            guna2Button7.FillColor = ColorTranslator.FromHtml("#DBEAFE");
            guna2Button7.Image = Properties.Resources.user;


        }
        private void label7_MouseEnter(object sender, EventArgs e)
        {
            guna2Button4.FillColor = Color.White;   // Màu khi hover
            guna2Button4.ForeColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button4.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button4.Image = Properties.Resources.arrow_right__1_;
            guna2Button7.FillColor = ColorTranslator.FromHtml("#2563EB");
            guna2Button7.Image = Properties.Resources.user__1_;

        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            guna2Button4.FillColor = Color.White;  // Màu ban đầu
            guna2Button4.ForeColor = Color.Black;
            guna2Button4.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
            guna2Button4.Image = null;
            guna2Button7.FillColor = ColorTranslator.FromHtml("#DBEAFE");
            guna2Button7.Image = Properties.Resources.user;
        }
        private void guna2Button5_MouseEnter(object sender, EventArgs e)
        {
            guna2Button8.FillColor = ColorTranslator.FromHtml("#059669");
            guna2Button8.Image = Properties.Resources.shield_check_white;


        }

        private void guna2Button5_MouseLeave(object sender, EventArgs e)
        {
            guna2Button8.FillColor = ColorTranslator.FromHtml("#D1FAE5");
            guna2Button8.Image = Properties.Resources.shield_check;
        }
        private void guna2Button8_MouseEnter(object sender, EventArgs e)
        {
            guna2Button5.FillColor = Color.White;   // Màu khi hover
            guna2Button5.ForeColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button5.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button5.Image = Properties.Resources.arrow_right__1_;

        }

        private void guna2Button8_MouseLeave(object sender, EventArgs e)
        {
            guna2Button5.FillColor = Color.White;  // Màu ban đầu
            guna2Button5.ForeColor = Color.Black;
            guna2Button5.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
            guna2Button5.Image = null;
        }
        private void label8_MouseEnter(object sender, EventArgs e)
        {
            guna2Button5.FillColor = Color.White;   // Màu khi hover
            guna2Button5.ForeColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button5.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button5.Image = Properties.Resources.arrow_right__1_;
            guna2Button8.FillColor = ColorTranslator.FromHtml("#059669");
            guna2Button8.Image = Properties.Resources.shield_check_white;

        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            guna2Button5.FillColor = Color.White;  // Màu ban đầu
            guna2Button5.ForeColor = Color.Black;
            guna2Button5.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
            guna2Button5.Image = null;
            guna2Button8.FillColor = ColorTranslator.FromHtml("#D1FAE5");
            guna2Button8.Image = Properties.Resources.shield_check;
        }
        private void guna2Button9_MouseEnter(object sender, EventArgs e)
        {
            guna2Button6.FillColor = Color.White;   // Màu khi hover
            guna2Button6.ForeColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button6.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button6.Image = Properties.Resources.arrow_right__1_;

        }

        private void guna2Button9_MouseLeave(object sender, EventArgs e)
        {
            guna2Button6.FillColor = Color.White;  // Màu ban đầu
            guna2Button6.ForeColor = Color.Black;
            guna2Button6.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
            guna2Button6.Image = null;
        }
        private void guna2Button6_MouseEnter(object sender, EventArgs e)
        {
            guna2Button9.FillColor = ColorTranslator.FromHtml("#D97706");
            guna2Button9.Image = Properties.Resources.shield__1_;


        }

        private void guna2Button6_MouseLeave(object sender, EventArgs e)
        {
            guna2Button9.FillColor = ColorTranslator.FromHtml("#FEF3C7");
            guna2Button9.Image = Properties.Resources.shield;
        }
        private void label9_MouseEnter(object sender, EventArgs e)
        {
            guna2Button6.FillColor = Color.White;   // Màu khi hover
            guna2Button6.ForeColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button6.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button6.Image = Properties.Resources.arrow_right__1_;
            guna2Button9.FillColor = ColorTranslator.FromHtml("#D97706");
            guna2Button9.Image = Properties.Resources.shield__1_;

        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            guna2Button6.FillColor = Color.White;  // Màu ban đầu
            guna2Button6.ForeColor = Color.Black;
            guna2Button6.BorderColor = ColorTranslator.FromHtml("#E2E8F0");
            guna2Button6.Image = null;
            guna2Button9.FillColor = ColorTranslator.FromHtml("#FEF3C7");
            guna2Button9.Image = Properties.Resources.shield;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            UC_dang_nhap_thu_thu dang_Nhap_Thu_Thu = new UC_dang_nhap_thu_thu();
            guna2Panel2.Controls.Add(dang_Nhap_Thu_Thu);
            dang_Nhap_Thu_Thu.BringToFront();
        }

        private void uC_dang_nhap_thu_thu1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            UC_doc_gia_dang_nhap doc_Gia_Dang_Nhap = new UC_doc_gia_dang_nhap();
            guna2Panel2.Controls.Add(doc_Gia_Dang_Nhap);
            doc_Gia_Dang_Nhap.BringToFront();

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            UC_Quan_li_dang_nhap quan_Li_Dang_Nhap = new UC_Quan_li_dang_nhap();
            guna2Panel2.Controls.Add(quan_Li_Dang_Nhap);
            quan_Li_Dang_Nhap.BringToFront();

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            UC_doc_gia_dang_nhap doc_Gia_Dang_Nhap = new UC_doc_gia_dang_nhap();
            guna2Panel2.Controls.Add(doc_Gia_Dang_Nhap);
            doc_Gia_Dang_Nhap.BringToFront();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            UC_doc_gia_dang_nhap doc_Gia_Dang_Nhap = new UC_doc_gia_dang_nhap();
            guna2Panel2.Controls.Add(doc_Gia_Dang_Nhap);
            doc_Gia_Dang_Nhap.BringToFront();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            UC_dang_nhap_thu_thu dang_Nhap_Thu_Thu = new UC_dang_nhap_thu_thu();
            guna2Panel2.Controls.Add(dang_Nhap_Thu_Thu);
            dang_Nhap_Thu_Thu.BringToFront();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            UC_dang_nhap_thu_thu dang_Nhap_Thu_Thu = new UC_dang_nhap_thu_thu();
            guna2Panel2.Controls.Add(dang_Nhap_Thu_Thu);
            dang_Nhap_Thu_Thu.BringToFront();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            UC_Quan_li_dang_nhap quan_Li_Dang_Nhap = new UC_Quan_li_dang_nhap();
            guna2Panel2.Controls.Add(quan_Li_Dang_Nhap);
            quan_Li_Dang_Nhap.BringToFront();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            UC_Quan_li_dang_nhap quan_Li_Dang_Nhap = new UC_Quan_li_dang_nhap();
            guna2Panel2.Controls.Add(quan_Li_Dang_Nhap);
            quan_Li_Dang_Nhap.BringToFront();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        

    }
}

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

namespace ThuVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = "\"Thư viện không chỉ là nơi giữ sách, đó là\nnơi giữ gìn tri thức và tâm hồn của nhân\nloại.\"";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            uC_dang_nhap_thu_thu1.Visible = true;
            uC_dang_nhap_thu_thu1.BringToFront();
        }

        private void uC_dang_nhap_thu_thu1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            uC_doc_gia_dang_nhap1.Visible = true;
            uC_doc_gia_dang_nhap1.BringToFront();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            uC_Quan_li_dang_nhap1.Visible=true;
            uC_Quan_li_dang_nhap1.BringToFront();
        }
        private void hover_doc_gia(object sender, EventArgs e)
        {
            guna2Button4.BorderColor = ColorTranslator.FromHtml("#4338CA");
            guna2Button4.ForeColor = ColorTranslator.FromHtml("#4338CA");
        }
        private void laeve_doc_gia(object sender, EventArgs e)
        {
            guna2Button7.FillColor = ColorTranslator.FromHtml("#DBEAFE");
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}

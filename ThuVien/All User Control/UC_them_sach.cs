using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThuVien.Models;

namespace ThuVien.All_User_Control
{
    public partial class UC_them_sach : UserControl
    {
        public UC_them_sach()
        {
            InitializeComponent();
        }
        public event Action SachDaDuocThem;
        private void UC_them_sach_Load(object sender, EventArgs e)
        {
   
             using (var context = new ThuVienContext())
            {
                var dsTheLoai = context.TheLoais.ToList();
                cbTheLoai.DataSource = dsTheLoai;
                cbTheLoai.DisplayMember = "TenTheLoai";
                cbTheLoai.ValueMember = "MaTheLoai";
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            using (var context = new ThuVienContext())
            {
                // Dùng ThuVien.Models.Sach thay vì Sach trùng tên
                var sach = new ThuVien.Models.Sach
                {
                    TenSach = txtTenSach.Text,
                    TacGia = txtTacGia.Text,
                    NhaXuatBan = txtNhaXuatBan.Text,
                    NamXuatBan = int.TryParse(txtNamXuatBan.Text, out int nam) ? nam : (int?)null,
                    SoLuong = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0,
                    MaTheLoai = (int?)cbTheLoai.SelectedValue
                };

                context.Saches.Add(sach);
                context.SaveChanges();
            }

            MessageBox.Show("Thêm sách thành công!");


            // Gọi event để UC_KhoSach biết cần reload
            SachDaDuocThem?.Invoke();

        }
    }
}

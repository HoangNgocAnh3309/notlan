using Microsoft.EntityFrameworkCore;
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
using ThuVien.All_User_Control;

namespace ThuVien.All_User_Control
{
    public partial class UC_KhoSach_FormQuanLy : UserControl

    {
        public UC_KhoSach_FormQuanLy()
        {
            InitializeComponent();

            flowLayoutPanel1.Padding = new Padding(0); // loại bỏ khoảng cách biên

            using (var context = new ThuVienContext())
            {

                var listSach = context.Saches
                    .Include(s => s.MaTheLoaiNavigation)
                    .ToList();

                flowLayoutPanel1.Controls.Clear();

                foreach (var sach in listSach)
                {
                    Sach uc = new Sach();   // ✅ UserControl

                    uc.Padding = new Padding(0);

                    uc.SetTenSach(sach.TenSach);
                    uc.SetTacGia(sach.TacGia ?? "Chưa có");
                    uc.SetTheLoai(sach.MaTheLoaiNavigation?.TenTheLoai ?? "Chưa có");
                    uc.SetNamXB(sach.NamXuatBan?.ToString() ?? "Chưa có");

                    // ===== TÍNH SỐ LƯỢNG ĐANG MƯỢN =====
                    int dangMuon = context.ChiTietPhieuMuons
                        .Where(ct => ct.MaSach == sach.MaSach
                            && ct.MaPhieuNavigation.TrangThai != "Đã trả")
                        .Sum(ct => (int?)ct.SoLuong) ?? 0;

                    int conLai = sach.SoLuong - dangMuon;

                    // ✅ set trạng thái theo số lượng còn
                    uc.SetTrangThaiTheoSoLuong(conLai);

                    uc.Tag = sach.MaSach;
                    flowLayoutPanel1.Controls.Add(uc);
                }
            }





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

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void XoaSach(int maSach)
        {
            using (var context = new ThuVienContext())
            {
                var sach = context.Saches.Find(maSach);
                if (sach != null)
                {
                    context.Saches.Remove(sach);
                    context.SaveChanges();
                }
            }

            LoadDanhSachSach(); // Reload danh sách sau khi xóa
        }

        public void LoadDanhSachSach()
        {
            flowLayoutPanel1.Controls.Clear();

            using (var context = new ThuVienContext())
            {

                var listSach = context.Saches
                    .Include(s => s.MaTheLoaiNavigation)
                    .ToList();

                flowLayoutPanel1.Controls.Clear();

                foreach (var sach in listSach)
                {
                    Sach uc = new Sach();   // ✅ UserControl

                    uc.Padding = new Padding(0);

                    uc.SetTenSach(sach.TenSach);
                    uc.MaSach = sach.MaSach;
                    uc.SetTacGia(sach.TacGia ?? "Chưa có");
                    uc.SetTheLoai(sach.MaTheLoaiNavigation?.TenTheLoai ?? "Chưa có");
                    uc.SetNamXB(sach.NamXuatBan?.ToString() ?? "Chưa có");

                    // ===== TÍNH SỐ LƯỢNG ĐANG MƯỢN =====
                    int dangMuon = context.ChiTietPhieuMuons
                        .Where(ct => ct.MaSach == sach.MaSach
                            && ct.MaPhieuNavigation.TrangThai != "Đã trả")
                        .Sum(ct => (int?)ct.SoLuong) ?? 0;

                    int conLai = sach.SoLuong - dangMuon;

                    // ✅ set trạng thái theo số lượng còn
                    uc.SetTrangThaiTheoSoLuong(conLai);

                    uc.Tag = sach.MaSach;
                    uc.XoaSachEvent += (id) =>
                    {
                        XoaSach(id);
                    };
                    flowLayoutPanel1.Controls.Add(uc);
                }
            }

        }

        private void btnThemSach_Click_2(object sender, EventArgs e)
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
            var ucThemSach = new UC_them_sach();
            ucThemSach.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(ucThemSach);
            ucThemSach.BringToFront();

            ucThemSach.SachDaDuocThem += () =>
            {
                LoadDanhSachSach(); // Hàm reload sách
                this.Parent.Controls.Remove(ucThemSach); // Ẩn UC_ThemSach sau khi thêm
            };
        }
    }
}

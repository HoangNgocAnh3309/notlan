using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            flowLayoutPanel1.Padding = new Padding(0);
            LoadDanhSachSach(); // THÊM DÒNG NÀY
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

        // THÊM PHƯƠNG THỨC NÀY: Xử lý xóa sách
        private void XoaSach(int maSach)
        {
            try
            {
                using (var context = new ThuVienContext())
                {
                    // Tìm sách cần xóa
                    var sach = context.Saches
                        .Include(s => s.ChiTietPhieuMuons)
                        .FirstOrDefault(s => s.MaSach == maSach);

                    if (sach == null)
                    {
                        MessageBox.Show("Không tìm thấy sách!");
                        return;
                    }

                    // Kiểm tra xem sách có đang được mượn không
                    bool dangDuocMuon = sach.ChiTietPhieuMuons
                        .Any(ct => ct.MaPhieuNavigation != null && ct.MaPhieuNavigation.TrangThai != "Đã trả");

                    if (dangDuocMuon)
                    {
                        MessageBox.Show("Không thể xóa sách đang được mượn!");
                        return;
                    }

                    // Xóa tất cả chi tiết phiếu mượn liên quan
                    if (sach.ChiTietPhieuMuons.Any())
                    {
                        context.ChiTietPhieuMuons.RemoveRange(sach.ChiTietPhieuMuons);
                    }

                    // Xóa sách
                    context.Saches.Remove(sach);
                    context.SaveChanges();

                    MessageBox.Show("Xóa sách thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }

            // Load lại danh sách sách sau khi xóa
            LoadDanhSachSach();
        }

        // THÊM PHƯƠNG THỨC SỬA SÁCH
        private void SuaSach(int maSach)
        {
            try
            {
                // Tạo form sửa sách và truyền mã sách
                suasach ss = new suasach(maSach); // SỬA DÒNG NÀY

                // Sử dụng ShowDialog để chờ form đóng
                ss.ShowDialog();

                // Sau khi form sửa đóng, reload danh sách
                LoadDanhSachSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form sửa: {ex.Message}");
            }
        }

        // SỬA PHƯƠNG THỨC LoadDanhSachSach
        public void LoadDanhSachSach()
        {
            flowLayoutPanel1.Controls.Clear();
            using (var context = new ThuVienContext())
            {
                var listSach = context.Saches
                    .Include(s => s.MaTheLoaiNavigation)
                    .ToList();

                foreach (var sach in listSach)
                {
                    Sach uc = new Sach();
                    uc.Padding = new Padding(0);

                    // Gán thông tin
                    uc.SetTenSach(sach.TenSach);
                    uc.MaSach = sach.MaSach; // QUAN TRỌNG: Gán mã sách
                    uc.SetTacGia(sach.TacGia ?? "Chưa có");
                    uc.SetTheLoai(sach.MaTheLoaiNavigation?.TenTheLoai ?? "Chưa có");
                    uc.SetNamXB(sach.NamXuatBan?.ToString() ?? "Chưa có");

                    // Tính số lượng đang mượn
                    int dangMuon = context.ChiTietPhieuMuons
                        .Where(ct => ct.MaSach == sach.MaSach && ct.MaPhieuNavigation.TrangThai != "Đã trả")
                        .Sum(ct => (int?)ct.SoLuong) ?? 0;

                    int conLai = sach.SoLuong - dangMuon;
                    uc.SetTrangThaiTheoSoLuong(conLai);

                    uc.Tag = sach.MaSach;

                    // THÊM DÒNG NÀY: Gắn sự kiện xóa
                    uc.XoaSachEvent += XoaSach;

                    // THÊM DÒNG NÀY: Gắn sự kiện sửa
                    uc.SuaSachEvent += SuaSach; // THÊM DÒNG NÀY

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
                LoadDanhSachSach();
                this.Parent.Controls.Remove(ucThemSach);
            };
        }
    }
}
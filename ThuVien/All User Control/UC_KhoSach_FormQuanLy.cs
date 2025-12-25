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

            // Đảm bảo FlowLayoutPanel có thiết lập đúng
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
        

            LoadDanhSachSach();
            LoadTheLoaiComboBox();

            // KẾT NỐI SỰ KIỆN TÌM KIẾM - THÊM DÒNG NÀY
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            cbTheLoai.SelectedIndexChanged += cbTheLoai_SelectedIndexChanged;
        }

        // THÊM PHƯƠNG THỨC: Load thể loại vào combobox
        private void LoadTheLoaiComboBox()
        {
            try
            {
                using (var context = new ThuVienContext())
                {
                    // Lấy danh sách thể loại từ database
                    var theLoais = context.TheLoais.ToList();

                    // Xóa các item cũ (nếu có)
                    cbTheLoai.Items.Clear();

                    // Thêm item "Tất cả" vào đầu
                    cbTheLoai.Items.Add("Tất cả");

                    // Thêm các thể loại từ database
                    foreach (var tl in theLoais)
                    {
                        cbTheLoai.Items.Add(tl.TenTheLoai);
                    }

                    // Chọn "Tất cả" mặc định
                    cbTheLoai.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load thể loại: {ex.Message}");
            }
        }

        // THÊM PHƯƠNG THỨC: Tìm kiếm và lọc sách
        private void TimKiemVaLocSach()
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                string theLoaiDuocChon = cbTheLoai.Text;

                // XÓA TẤT CẢ CONTROL CŨ
                flowLayoutPanel1.Controls.Clear();

                using (var context = new ThuVienContext())
                {
                    // Lấy tất cả sách
                    var query = context.Saches
                        .Include(s => s.MaTheLoaiNavigation)
                        .AsQueryable();

                    // Tìm kiếm theo từ khóa (nếu có) - SỬA: Dùng ToLower()
                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        string tuKhoaLower = tuKhoa.ToLower();
                        query = query.Where(s =>
                            s.TenSach.ToLower().Contains(tuKhoaLower) ||
                            (s.TacGia != null && s.TacGia.ToLower().Contains(tuKhoaLower)));
                    }

                    // Lọc theo thể loại (nếu không chọn "Tất cả")
                    if (theLoaiDuocChon != "Tất cả" && !string.IsNullOrEmpty(theLoaiDuocChon))
                    {
                        query = query.Where(s =>
                            s.MaTheLoaiNavigation != null &&
                            s.MaTheLoaiNavigation.TenTheLoai == theLoaiDuocChon);
                    }

                    var listSach = query.ToList();

                    // DEBUG: Hiển thị số lượng sách tìm được
                    Console.WriteLine($"DEBUG: Tìm thấy {listSach.Count} sách");

                    // Kiểm tra nếu không có sách nào
                    if (listSach.Count == 0)
                    {
                        Label lblNoResult = new Label();
                        lblNoResult.Text = "Không tìm thấy sách nào!";
                        lblNoResult.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
                        lblNoResult.ForeColor = System.Drawing.Color.Red;
                        lblNoResult.AutoSize = true;
                        flowLayoutPanel1.Controls.Add(lblNoResult);
                        return;
                    }

                    foreach (var sach in listSach)
                    {
                        Sach uc = new Sach();

                        // THIẾT LẬP KÍCH THƯỚC


                        // Gán thông tin
                        uc.SetTenSach(sach.TenSach);
                        uc.MaSach = sach.MaSach;
                        uc.SetTacGia(sach.TacGia ?? "Chưa có");
                        uc.SetTheLoai(sach.MaTheLoaiNavigation?.TenTheLoai ?? "Chưa có");
                        uc.SetNamXB(sach.NamXuatBan?.ToString() ?? "Chưa có");

                        // Tính số lượng đang mượn
                        int dangMuon = context.ChiTietPhieuMuons
                            .Include(ct => ct.MaPhieuNavigation)
                            .Where(ct => ct.MaSach == sach.MaSach &&
                                   ct.MaPhieuNavigation != null &&
                                   ct.MaPhieuNavigation.TrangThai != "Đã trả")
                            .Sum(ct => (int?)ct.SoLuong) ?? 0;

                        int conLai = sach.SoLuong - dangMuon;
                        uc.SetTrangThaiTheoSoLuong(conLai);

                        uc.Tag = sach.MaSach;

                        // Gắn sự kiện
                        uc.XoaSachEvent += XoaSach;
                        uc.SuaSachEvent += SuaSach;

                        // DEBUG: In ra console
                        Console.WriteLine($"DEBUG: Thêm sách - {sach.TenSach}");

                        // THÊM VÀO FLOWLAYOUT
                        flowLayoutPanel1.Controls.Add(uc);
                    }

                    // Cập nhật layout
                    flowLayoutPanel1.PerformLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}");
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        // THÊM SỰ KIỆN: TextBox tìm kiếm thay đổi
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Gọi tìm kiếm khi người dùng nhập
            TimKiemVaLocSach();
        }

        // THÊM SỰ KIỆN: ComboBox thể loại thay đổi
        private void cbTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiemVaLocSach();
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
                suasach ss = new suasach(maSach);

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
            // Reset tìm kiếm
            txtTimKiem.Text = "";
            if (cbTheLoai.Items.Count > 0)
                cbTheLoai.SelectedIndex = 0;

            // Gọi tìm kiếm để hiển thị tất cả
            TimKiemVaLocSach();
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

        // THÊM SỰ KIỆN: Guna2TextBox1 (nếu đây là textbox tìm kiếm)
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Có thể đổi tên sự kiện này nếu cần
        }
    }
}
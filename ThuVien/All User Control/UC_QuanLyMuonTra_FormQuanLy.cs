using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThuVien.Models;

namespace ThuVien.All_User_Control
{
    public partial class UC_QuanLyMuonTra_FormQuanLy : UserControl
    {
        public UC_QuanLyMuonTra_FormQuanLy()
        {
            InitializeComponent();
            TimKiemPhieuMuon();

            // 🔹 Gắn event CheckedChanged cho các nút Guna2Button
            btnTatCa.CheckedChanged += BtnTatCa_CheckedChanged;
            btnChoMuon.CheckedChanged += BtnChoMuon_CheckedChanged;
            btnLichSu.CheckedChanged += BtnLichSu_CheckedChanged;

            // 🔹 Gắn event TextChanged cho TextBox tìm kiếm
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;
        }

        private void UC_QuanLyMuonTra_FormQuanLy_Load(object sender, EventArgs e)
        {
            btnTatCa.Checked = true; // Trigger load Tất cả khi UC load
        }

        // =========================
        // CheckedChanged Events
        // =========================
        private void BtnTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (!btnTatCa.Checked) return;
            TimKiemPhieuMuon();
        }

        private void BtnChoMuon_CheckedChanged(object sender, EventArgs e)
        {
            if (!btnChoMuon.Checked) return;
            TimKiemPhieuMuon();
        }

        private void BtnLichSu_CheckedChanged(object sender, EventArgs e)
        {
            if (!btnLichSu.Checked) return;
            TimKiemPhieuMuon();
        }

        // =========================
        // TÌM KIẾM THEO TÊN, MÃ ĐỘC GIẢ HOẶC TÊN SÁCH
        // =========================
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            TimKiemPhieuMuon();
        }

        private void TimKiemPhieuMuon()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                string tuKhoa = guna2TextBox1.Text.Trim().ToLower();
                string trangThai = GetTrangThaiHienTai();

                using (var context = new ThuVienContext())
                {
                    var query = context.PhieuMuons
                        .Include(p => p.MaDocGiaNavigation)
                        .Include(p => p.ChiTietPhieuMuons)
                            .ThenInclude(ct => ct.MaSachNavigation)
                        .AsQueryable();

                    // Lọc theo trạng thái
                    if (trangThai == "Đang mượn")
                    {
                        query = query.Where(p => (p.TrangThai ?? "").Trim() == "Đang mượn" ||
                                                (p.TrangThai ?? "").Trim() == "Quá hạn");
                    }
                    else if (trangThai == "Đã trả")
                    {
                        query = query.Where(p => (p.TrangThai ?? "").Trim() == "Đã trả");
                    }
                    // "Tất cả" thì không lọc trạng thái

                    // Tìm kiếm theo từ khóa (nếu có)
                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        query = query.Where(p =>
                            // Tìm theo tên độc giả
                            (p.MaDocGiaNavigation != null &&
                             p.MaDocGiaNavigation.TenDocGia.ToLower().Contains(tuKhoa)) ||

                            // Tìm theo mã độc giả (chuyển sang string để tìm)
                            (p.MaDocGia.ToString().Contains(tuKhoa)) ||

                            // Tìm theo tên sách
                            (p.ChiTietPhieuMuons.Any(ct =>
                                ct.MaSachNavigation != null &&
                                ct.MaSachNavigation.TenSach.ToLower().Contains(tuKhoa)))
                        );
                    }

                    var phieuMuons = query.OrderByDescending(p => p.NgayMuon).ToList();

                    if (!phieuMuons.Any())
                    {
                        ShowNoDataMessage("📭 Không tìm thấy kết quả phù hợp!");
                        return;
                    }

                    foreach (var phieu in phieuMuons)
                    {
                        AddPhieuMuonToFlowPanel(phieu);
                    }

                    // Hiển thị số lượng kết quả
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}");
            }
        }

        // =========================
        // Lấy trạng thái hiện tại
        // =========================
        private string GetTrangThaiHienTai()
        {
            if (btnTatCa.Checked) return "Tất cả";
            if (btnChoMuon.Checked) return "Đang mượn";
            if (btnLichSu.Checked) return "Đã trả";
            return "Tất cả";
        }

        // =========================
        // Add UC_muonsach với event trả sách
        // =========================
        private void AddPhieuMuonToFlowPanel(PhieuMuon phieu)
        {
            string tenSach = "Không có thông tin sách";
            var sachDauTien = phieu.ChiTietPhieuMuons.FirstOrDefault();
            if (sachDauTien != null && sachDauTien.MaSachNavigation != null)
            {
                tenSach = sachDauTien.MaSachNavigation.TenSach;
                if (phieu.ChiTietPhieuMuons.Count > 1)
                {
                    tenSach += $" (+{phieu.ChiTietPhieuMuons.Count - 1})";
                }
            }

            UC_muonsach uc = new UC_muonsach();
            uc.SetTenDocGia(phieu.MaDocGiaNavigation?.TenDocGia ?? "Không xác định");
            uc.SetMaDocGia(phieu.MaDocGia);
            uc.SetTenSach(tenSach);
            uc.SetNgayMuon(phieu.NgayMuon);
            uc.SetNgayTraDuKien(phieu.NgayTraDuKien);
            uc.SetTrangThai(phieu.TrangThai ?? "Không xác định");

            // Gán mã phiếu
            uc.MaPhieu = phieu.MaPhieu;

            // Gắn sự kiện trả sách (chỉ khi trạng thái không phải "Đã trả")
            if (phieu.TrangThai != "Đã trả")
            {
                uc.TraSachEvent += TraSach;
            }

            flowLayoutPanel1.Controls.Add(uc);
        }

        // =========================
        // Xử lý trả sách
        // =========================
        private void TraSach(int maPhieu)
        {
            try
            {
                using (var context = new ThuVienContext())
                {
                    // Tìm phiếu mượn
                    var phieu = context.PhieuMuons
                        .Include(p => p.MaDocGiaNavigation)
                        .FirstOrDefault(p => p.MaPhieu == maPhieu);

                    if (phieu == null)
                    {
                        MessageBox.Show("Không tìm thấy phiếu mượn!");
                        return;
                    }

                    // Hiển thị xác nhận
                    var result = MessageBox.Show(
                        $"Xác nhận trả sách cho độc giả '{phieu.MaDocGiaNavigation?.TenDocGia}'?",
                        "Xác nhận trả sách",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes) return;

                    // Cập nhật thông tin trả sách
                    phieu.NgayTraThucTe = DateTime.Now.Date;
                    phieu.TrangThai = "Đã trả";

                    // Cập nhật số lượng sách trong kho
                    var chiTietPhieu = context.ChiTietPhieuMuons
                        .Where(ct => ct.MaPhieu == maPhieu)
                        .ToList();

                    foreach (var ct in chiTietPhieu)
                    {
                        var sach = context.Saches.Find(ct.MaSach);
                        if (sach != null)
                        {
                            sach.SoLuong += ct.SoLuong;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("✅ Trả sách thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload tìm kiếm (giữ nguyên từ khóa và tab)
                    TimKiemPhieuMuon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi trả sách: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // Hiển thị thông báo
        // =========================
        private void ShowNoDataMessage(string message)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Gray,
                AutoSize = true,
                Margin = new Padding(20),
                TextAlign = ContentAlignment.MiddleCenter
            });
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FormMuonSach uc = new FormMuonSach();
            uc.Visible = true;
            uc.BringToFront();
        }
    }
}
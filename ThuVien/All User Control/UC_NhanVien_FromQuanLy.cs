using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThuVien.Models;

namespace ThuVien.All_User_Control
{
    public partial class UC_NhanVien_FromQuanLy : UserControl
    {
        private ThuVienContext dbContext;

        public UC_NhanVien_FromQuanLy()
        {
            InitializeComponent();
            dbContext = new ThuVienContext();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Clear existing controls
                flowLayoutPanelNhanVien.Controls.Clear();

                // Load all employees from database
                var nhanViens = dbContext.NhanViens
                    .OrderByDescending(nv => nv.ChucVu != null &&
                                           (nv.ChucVu.Contains("Quản lý") || nv.ChucVu.Contains("Admin")))
                    .ThenBy(nv => nv.TenNhanVien)
                    .ToList();

                // Display statistics
                UpdateStatistics(nhanViens);

                // If no employees found
                if (nhanViens.Count == 0)
                {
                    ShowNoDataMessage();
                    return;
                }

                // Create UC_NhanVien for each employee
                foreach (var nhanVien in nhanViens)
                {
                    AddNhanVienToFlowLayout(nhanVien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu nhân viên: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics(System.Collections.Generic.List<NhanVien> nhanViens)
        {
            lblTongSoNhanVien.Text = $"Tổng số: {nhanViens.Count} nhân viên";
            lblQuanLy.Text = $"Quản lý: {nhanViens.Count(nv =>
                !string.IsNullOrEmpty(nv.ChucVu) &&
                (nv.ChucVu.Contains("Quản lý") || nv.ChucVu.Contains("Admin")))}";
            lblNhanVien.Text = $"Nhân viên: {nhanViens.Count(nv =>
                string.IsNullOrEmpty(nv.ChucVu) ||
                (!nv.ChucVu.Contains("Quản lý") && !nv.ChucVu.Contains("Admin")))}";
        }

        private void ShowNoDataMessage()
        {
            var lblEmpty = new Label
            {
                Text = "Chưa có nhân viên nào trong hệ thống",
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(flowLayoutPanelNhanVien.Width - 40, 100),
                AutoSize = false
            };

            flowLayoutPanelNhanVien.Controls.Add(lblEmpty);
        }

        private void AddNhanVienToFlowLayout(NhanVien nhanVien)
        {
            try
            {
                // Tạo UC_NhanVien
                var ucNhanVien = new UC_NhanVien();

                // Set thông tin
                ucNhanVien.SetThongTin(nhanVien.TenNhanVien, nhanVien.ChucVu);

                // Lưu đối tượng NhanVien trong Tag
                ucNhanVien.Tag = nhanVien;

                // Đăng ký sự kiện xem chi tiết
                ucNhanVien.OnXemChiTiet += (sender, e) =>
                {
                    ShowNhanVienDetails(nhanVien);
                };

                // Đặt kích thước
                ucNhanVien.Width = 400;
                ucNhanVien.Height = 252;
                ucNhanVien.Margin = new Padding(15);

                // Thêm vào FlowLayoutPanel
                flowLayoutPanelNhanVien.Controls.Add(ucNhanVien);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tạo UC_NhanVien: {ex.Message}");
            }
        }

        private void ShowNhanVienDetails(NhanVien nhanVien)
        {
            // Tạo form chi tiết đơn giản
            using (var form = new Form())
            {
                form.Text = $"Chi tiết nhân viên: {nhanVien.TenNhanVien}";
                form.Size = new Size(500, 400);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20)
                };

                var lblTitle = new Label
                {
                    Text = $"THÔNG TIN NHÂN VIÊN",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Location = new Point(0, 0),
                    Size = new Size(440, 30),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var lblMa = new Label
                {
                    Text = $"Mã nhân viên: {nhanVien.MaNhanVien:0000}",
                    Location = new Point(0, 50),
                    Size = new Size(200, 25)
                };

                var lblTen = new Label
                {
                    Text = $"Tên: {nhanVien.TenNhanVien}",
                    Location = new Point(0, 85),
                    Size = new Size(400, 25),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold)
                };

                var lblChucVu = new Label
                {
                    Text = $"Chức vụ: {nhanVien.ChucVu ?? "Nhân viên"}",
                    Location = new Point(0, 120),
                    Size = new Size(300, 25)
                };

                var lblSDT = new Label
                {
                    Text = $"SĐT: {nhanVien.Sdt ?? "Chưa có"}",
                    Location = new Point(0, 155),
                    Size = new Size(300, 25)
                };

                var lblEmail = new Label
                {
                    Text = $"Email: {nhanVien.Email ?? "Chưa có"}",
                    Location = new Point(0, 190),
                    Size = new Size(300, 25)
                };

                var lblTaiKhoan = new Label
                {
                    Text = $"Tài khoản: {nhanVien.TaiKhoan ?? "Chưa có"}",
                    Location = new Point(0, 225),
                    Size = new Size(300, 25)
                };

                var btnSua = new Button
                {
                    Text = "Sửa",
                    Location = new Point(100, 270),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(33, 150, 243),
                    ForeColor = Color.White
                };

                var btnXoa = new Button
                {
                    Text = "Xóa",
                    Location = new Point(210, 270),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(244, 67, 54),
                    ForeColor = Color.White
                };

                var btnDong = new Button
                {
                    Text = "Đóng",
                    Location = new Point(320, 270),
                    Size = new Size(100, 35)
                };

                btnSua.Click += (s, e) =>
                {
                    SuaThongTinNhanVien(nhanVien);
                    form.Close();
                };

                btnXoa.Click += (s, e) =>
                {
                    if (MessageBox.Show($"Xóa nhân viên '{nhanVien.TenNhanVien}'?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            dbContext.NhanViens.Remove(nhanVien);
                            dbContext.SaveChanges();
                            MessageBox.Show("Đã xóa thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            form.DialogResult = DialogResult.OK;
                            form.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };

                btnDong.Click += (s, e) => form.Close();

                panel.Controls.Add(lblTitle);
                panel.Controls.Add(lblMa);
                panel.Controls.Add(lblTen);
                panel.Controls.Add(lblChucVu);
                panel.Controls.Add(lblSDT);
                panel.Controls.Add(lblEmail);
                panel.Controls.Add(lblTaiKhoan);
                panel.Controls.Add(btnSua);
                panel.Controls.Add(btnXoa);
                panel.Controls.Add(btnDong);

                form.Controls.Add(panel);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void SuaThongTinNhanVien(NhanVien nhanVien)
        {
            using (var form = new Form())
            {
                form.Text = "Sửa thông tin nhân viên";
                form.Size = new Size(500, 350);
                form.StartPosition = FormStartPosition.CenterParent;

                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20)
                };

                var lblTen = new Label { Text = "Tên nhân viên:*", Location = new Point(0, 0), Size = new Size(150, 25) };
                var txtTen = new TextBox { Location = new Point(150, 0), Size = new Size(300, 25), Text = nhanVien.TenNhanVien };

                var lblChucVu = new Label { Text = "Chức vụ:", Location = new Point(0, 40), Size = new Size(150, 25) };
                var cbChucVu = new ComboBox
                {
                    Location = new Point(150, 40),
                    Size = new Size(200, 25),
                    Items = { "Nhân viên", "Quản lý", "Trưởng phòng", "Admin" }
                };
                cbChucVu.SelectedItem = nhanVien.ChucVu ?? "Nhân viên";

                var lblSDT = new Label { Text = "Số điện thoại:", Location = new Point(0, 80), Size = new Size(150, 25) };
                var txtSDT = new TextBox { Location = new Point(150, 80), Size = new Size(300, 25), Text = nhanVien.Sdt ?? "" };

                var lblEmail = new Label { Text = "Email:", Location = new Point(0, 120), Size = new Size(150, 25) };
                var txtEmail = new TextBox { Location = new Point(150, 120), Size = new Size(300, 25), Text = nhanVien.Email ?? "" };

                var btnLuu = new Button
                {
                    Text = "Lưu",
                    Location = new Point(150, 180),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.OK,
                    BackColor = Color.FromArgb(76, 175, 80),
                    ForeColor = Color.White
                };

                var btnHuy = new Button
                {
                    Text = "Hủy",
                    Location = new Point(260, 180),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel
                };

                btnLuu.Click += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(txtTen.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên nhân viên!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                };

                panel.Controls.Add(lblTen);
                panel.Controls.Add(txtTen);
                panel.Controls.Add(lblChucVu);
                panel.Controls.Add(cbChucVu);
                panel.Controls.Add(lblSDT);
                panel.Controls.Add(txtSDT);
                panel.Controls.Add(lblEmail);
                panel.Controls.Add(txtEmail);
                panel.Controls.Add(btnLuu);
                panel.Controls.Add(btnHuy);

                form.Controls.Add(panel);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        nhanVien.TenNhanVien = txtTen.Text.Trim();
                        nhanVien.ChucVu = cbChucVu.SelectedItem?.ToString();
                        nhanVien.Sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                        nhanVien.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();

                        dbContext.SaveChanges();
                        MessageBox.Show("Đã cập nhật thành công!", "Thành công");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemNhanVien();
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TimKiemNhanVien();
                e.Handled = true;
            }
        }

        private void TimKiemNhanVien()
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                LoadData();
                return;
            }

            try
            {
                flowLayoutPanelNhanVien.Controls.Clear();

                var ketQua = dbContext.NhanViens
                    .Where(nv => nv.TenNhanVien.Contains(tuKhoa) ||
                                 (nv.Sdt != null && nv.Sdt.Contains(tuKhoa)) ||
                                 (nv.Email != null && nv.Email.Contains(tuKhoa)) ||
                                 (nv.ChucVu != null && nv.ChucVu.Contains(tuKhoa)) ||
                                 (nv.TaiKhoan != null && nv.TaiKhoan.Contains(tuKhoa)))
                    .ToList();

                if (ketQua.Count == 0)
                {
                    ShowNoDataMessage();
                    lblTongSoNhanVien.Text = "Kết quả tìm kiếm: 0 nhân viên";
                    lblQuanLy.Text = "Quản lý: 0";
                    lblNhanVien.Text = "Nhân viên: 0";
                    return;
                }

                UpdateStatistics(ketQua);

                foreach (var nhanVien in ketQua)
                {
                    AddNhanVienToFlowLayout(nhanVien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadData();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV Files|*.csv|Text Files|*.txt";
                    sfd.Title = "Xuất danh sách nhân viên";
                    sfd.FileName = $"DanhSachNhanVien_{DateTime.Now:yyyyMMdd}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var nhanViens = dbContext.NhanViens.ToList();
                        string content = "Mã,Tên nhân viên,Chức vụ,SĐT,Email,Tài khoản\n";

                        foreach (var nv in nhanViens)
                        {
                            content += $"{nv.MaNhanVien},{nv.TenNhanVien},{nv.ChucVu ?? ""},{nv.Sdt ?? ""},{nv.Email ?? ""},{nv.TaiKhoan ?? ""}\n";
                        }

                        System.IO.File.WriteAllText(sfd.FileName, content, System.Text.Encoding.UTF8);

                        MessageBox.Show($"Đã xuất {nhanViens.Count} nhân viên!\nFile: {sfd.FileName}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Thêm nhân viên mới";
                form.Size = new Size(500, 400);
                form.StartPosition = FormStartPosition.CenterParent;

                var panel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20)
                };

                var lblTen = new Label { Text = "Tên nhân viên:*", Location = new Point(0, 0), Size = new Size(150, 25) };
                var txtTen = new TextBox { Location = new Point(150, 0), Size = new Size(300, 25) };

                var lblChucVu = new Label { Text = "Chức vụ:", Location = new Point(0, 40), Size = new Size(150, 25) };
                var cbChucVu = new ComboBox
                {
                    Location = new Point(150, 40),
                    Size = new Size(200, 25),
                    Items = { "Nhân viên", "Quản lý", "Trưởng phòng", "Admin" },
                    SelectedIndex = 0
                };

                var lblSDT = new Label { Text = "Số điện thoại:", Location = new Point(0, 80), Size = new Size(150, 25) };
                var txtSDT = new TextBox { Location = new Point(150, 80), Size = new Size(300, 25) };

                var lblEmail = new Label { Text = "Email:", Location = new Point(0, 120), Size = new Size(150, 25) };
                var txtEmail = new TextBox { Location = new Point(150, 120), Size = new Size(300, 25) };

                var lblTaiKhoan = new Label { Text = "Tài khoản:*", Location = new Point(0, 160), Size = new Size(150, 25) };
                var txtTaiKhoan = new TextBox { Location = new Point(150, 160), Size = new Size(300, 25) };

                var lblMatKhau = new Label { Text = "Mật khẩu:*", Location = new Point(0, 200), Size = new Size(150, 25) };
                var txtMatKhau = new TextBox
                {
                    Location = new Point(150, 200),
                    Size = new Size(300, 25),
                    UseSystemPasswordChar = true
                };

                var btnThem = new Button
                {
                    Text = "Thêm",
                    Location = new Point(150, 250),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.OK,
                    BackColor = Color.FromArgb(76, 175, 80),
                    ForeColor = Color.White
                };

                var btnHuy = new Button
                {
                    Text = "Hủy",
                    Location = new Point(260, 250),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel
                };

                btnThem.Click += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(txtTen.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên nhân viên!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTen.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tài khoản!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTaiKhoan.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMatKhau.Focus();
                        return;
                    }
                };

                panel.Controls.Add(lblTen);
                panel.Controls.Add(txtTen);
                panel.Controls.Add(lblChucVu);
                panel.Controls.Add(cbChucVu);
                panel.Controls.Add(lblSDT);
                panel.Controls.Add(txtSDT);
                panel.Controls.Add(lblEmail);
                panel.Controls.Add(txtEmail);
                panel.Controls.Add(lblTaiKhoan);
                panel.Controls.Add(txtTaiKhoan);
                panel.Controls.Add(lblMatKhau);
                panel.Controls.Add(txtMatKhau);
                panel.Controls.Add(btnThem);
                panel.Controls.Add(btnHuy);

                form.Controls.Add(panel);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Kiểm tra tài khoản đã tồn tại
                        var existing = dbContext.NhanViens.FirstOrDefault(nv => nv.TaiKhoan == txtTaiKhoan.Text.Trim());
                        if (existing != null)
                        {
                            MessageBox.Show("Tài khoản đã tồn tại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Tạo nhân viên mới
                        var nhanVienMoi = new NhanVien
                        {
                            TenNhanVien = txtTen.Text.Trim(),
                            ChucVu = cbChucVu.SelectedItem?.ToString(),
                            Sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim(),
                            Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                            TaiKhoan = txtTaiKhoan.Text.Trim(),
                            MatKhau = txtMatKhau.Text.Trim()
                        };

                        dbContext.NhanViens.Add(nhanVienMoi);
                        dbContext.SaveChanges();

                        MessageBox.Show($"Đã thêm nhân viên thành công!\nTài khoản: {nhanVienMoi.TaiKhoan}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
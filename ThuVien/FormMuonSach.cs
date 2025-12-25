using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using ThuVien.Models;

namespace ThuVien.All_User_Control
{
    public partial class FormMuonSach : Form
    {
        private List<ChiTietMuon> danhSachMuon = new List<ChiTietMuon>();
        private int soNgayMuonMacDinh = 14;

        public class ChiTietMuon
        {
            public int MaSach { get; set; }
            public string TenSach { get; set; }
            public int SoLuong { get; set; }
            public int SoLuongCon { get; set; }
            public string TacGia { get; set; }
            public string TheLoai { get; set; }
        }

        public FormMuonSach()
        {
            InitializeComponent();
            LoadDuLieuKhoiTao();
        }

        private void LoadDuLieuKhoiTao()
        {
            try
            {
                dtpNgayMuon.Value = DateTime.Now.Date;
                dtpNgayTraDuKien.Value = DateTime.Now.Date.AddDays(soNgayMuonMacDinh);
                LoadNhanVien();
                LoadDataGridView();
                numSoNgayMuon.Value = soNgayMuonMacDinh;
                txtMaDocGia.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVien()
        {
            try
            {
                using (var context = new ThuVienContext())
                {
                    var nhanViens = context.NhanViens
                        .OrderBy(n => n.TenNhanVien)
                        .ToList();

                    cbNhanVien.DataSource = nhanViens;
                    cbNhanVien.DisplayMember = "TenNhanVien";
                    cbNhanVien.ValueMember = "MaNhanVien";

                    if (cbNhanVien.Items.Count > 0)
                        cbNhanVien.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load nhân viên: {ex.Message}");
            }
        }

        private void btnTimDocGia_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtMaDocGia.Text.Trim();

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    TimKiemDocGiaNangCao();
                    return;
                }

                TimKiemDocGia(tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm độc giả: {ex.Message}");
            }
        }

        private void TimKiemDocGiaNangCao()
        {
            using (var form = new Form())
            {
                form.Text = "Tìm kiếm độc giả";
                form.Size = new Size(500, 400);
                form.StartPosition = FormStartPosition.CenterParent;

                var txtTim = new TextBox();
                txtTim.Location = new Point(20, 20);
                txtTim.Size = new Size(300, 30);
                txtTim.PlaceholderText = "Nhập tên, mã hoặc SDT...";

                var btnTim = new Button();
                btnTim.Text = "Tìm";
                btnTim.Location = new Point(330, 20);
                btnTim.Size = new Size(80, 30);

                var dgv = new DataGridView();
                dgv.Location = new Point(20, 60);
                dgv.Size = new Size(440, 250);
                dgv.ReadOnly = true;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoGenerateColumns = false;

                dgv.Columns.Add("MaDocGia", "Mã");
                dgv.Columns.Add("TenDocGia", "Tên độc giả");
                dgv.Columns.Add("Sdt", "SĐT");
                dgv.Columns.Add("Email", "Email");

                var btnChon = new Button();
                btnChon.Text = "Chọn";
                btnChon.Location = new Point(180, 320);
                btnChon.Size = new Size(80, 30);
                btnChon.DialogResult = DialogResult.OK;

                var btnHuy = new Button();
                btnHuy.Text = "Hủy";
                btnHuy.Location = new Point(280, 320);
                btnHuy.Size = new Size(80, 30);
                btnHuy.DialogResult = DialogResult.Cancel;

                var btnTaoMoi = new Button();
                btnTaoMoi.Text = "Tạo mới";
                btnTaoMoi.Location = new Point(20, 320);
                btnTaoMoi.Size = new Size(80, 30);

                form.Controls.AddRange(new Control[] { txtTim, btnTim, dgv, btnChon, btnHuy, btnTaoMoi });

                btnTim.Click += (s, ev) =>
                {
                    using (var context = new ThuVienContext())
                    {
                        var tuKhoa = txtTim.Text.Trim();
                        var query = context.DocGia.AsQueryable();

                        if (!string.IsNullOrEmpty(tuKhoa))
                        {
                            if (int.TryParse(tuKhoa, out int maDocGia))
                            {
                                query = query.Where(d => d.MaDocGia == maDocGia);
                            }
                            else
                            {
                                query = query.Where(d => d.TenDocGia.Contains(tuKhoa) ||
                                                       d.Sdt.Contains(tuKhoa));
                            }
                        }

                        var result = query.ToList();
                        dgv.DataSource = result;
                    }
                };

                btnTaoMoi.Click += (s, ev) =>
                {
                    var docGiaMoi = TaoMoiDocGia();
                    if (docGiaMoi != null)
                    {
                        form.DialogResult = DialogResult.OK;
                        form.Tag = docGiaMoi;
                        form.Close();
                    }
                };

                dgv.CellDoubleClick += (s, ev) =>
                {
                    if (ev.RowIndex >= 0)
                    {
                        form.DialogResult = DialogResult.OK;
                        form.Tag = dgv.Rows[ev.RowIndex].DataBoundItem;
                        form.Close();
                    }
                };

                if (form.ShowDialog() == DialogResult.OK && form.Tag != null)
                {
                    if (form.Tag is DocGium docGia)
                    {
                        HienThiThongTinDocGia(docGia);
                    }
                }
            }
        }

        private DocGium TaoMoiDocGia()
        {
            using (var form = new Form())
            {
                form.Text = "Tạo độc giả mới";
                form.Size = new Size(400, 350);
                form.StartPosition = FormStartPosition.CenterParent;

                var lblTen = new Label { Text = "Tên độc giả:*", Location = new Point(20, 20), Size = new Size(100, 25) };
                var txtTen = new TextBox { Location = new Point(130, 20), Size = new Size(220, 25) };

                var lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(20, 60), Size = new Size(100, 25) };
                var dtpNgaySinh = new DateTimePicker
                {
                    Location = new Point(130, 60),
                    Size = new Size(220, 25),
                    Value = DateTime.Now.AddYears(-18)
                };

                var lblGioiTinh = new Label { Text = "Giới tính:", Location = new Point(20, 100), Size = new Size(100, 25) };
                var cbGioiTinh = new ComboBox
                {
                    Location = new Point(130, 100),
                    Size = new Size(100, 25),
                    Items = { "Nam", "Nữ", "Khác" },
                    SelectedIndex = 0
                };

                var lblSDT = new Label { Text = "SĐT:", Location = new Point(20, 140), Size = new Size(100, 25) };
                var txtSDT = new TextBox { Location = new Point(130, 140), Size = new Size(220, 25) };

                var lblEmail = new Label { Text = "Email:", Location = new Point(20, 180), Size = new Size(100, 25) };
                var txtEmail = new TextBox { Location = new Point(130, 180), Size = new Size(220, 25) };

                var lblDiaChi = new Label { Text = "Địa chỉ:", Location = new Point(20, 220), Size = new Size(100, 25) };
                var txtDiaChi = new TextBox { Location = new Point(130, 220), Size = new Size(220, 25) };

                var btnOK = new Button { Text = "OK", Location = new Point(130, 260), Size = new Size(80, 30), DialogResult = DialogResult.OK };
                var btnHuy = new Button { Text = "Hủy", Location = new Point(220, 260), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

                form.Controls.AddRange(new Control[]
                {
                    lblTen, txtTen, lblNgaySinh, dtpNgaySinh, lblGioiTinh, cbGioiTinh,
                    lblSDT, txtSDT, lblEmail, txtEmail, lblDiaChi, txtDiaChi,
                    btnOK, btnHuy
                });

                btnOK.Click += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(txtTen.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên độc giả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var context = new ThuVienContext())
                        {
                            var docGia = new DocGium
                            {
                                TenDocGia = txtTen.Text.Trim(),
                                NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value),
                                GioiTinh = cbGioiTinh.SelectedItem?.ToString(),
                                Sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim(),
                                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim()
                            };

                            context.DocGia.Add(docGia);
                            context.SaveChanges();

                            return context.DocGia.FirstOrDefault(d => d.MaDocGia == docGia.MaDocGia);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tạo độc giả: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return null;
            }
        }

        private void TimKiemDocGia(string tuKhoa)
        {
            using (var context = new ThuVienContext())
            {
                DocGium docGia = null;

                if (int.TryParse(tuKhoa, out int maDocGia))
                {
                    docGia = context.DocGia
                        .FirstOrDefault(d => d.MaDocGia == maDocGia);
                }

                if (docGia == null)
                {
                    docGia = context.DocGia
                        .FirstOrDefault(d => d.TenDocGia.Contains(tuKhoa));
                }

                if (docGia == null)
                {
                    docGia = context.DocGia
                        .FirstOrDefault(d => d.Sdt != null && d.Sdt.Contains(tuKhoa));
                }

                if (docGia != null)
                {
                    HienThiThongTinDocGia(docGia);
                    lblThongTin.Text = $"✓ Đã tìm thấy: {docGia.TenDocGia}";
                    lblThongTin.ForeColor = Color.Green;
                }
                else
                {
                    var result = MessageBox.Show(
                        "Không tìm thấy độc giả. Bạn có muốn tạo mới?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var docGiaMoi = TaoMoiDocGia();
                        if (docGiaMoi != null)
                        {
                            HienThiThongTinDocGia(docGiaMoi);
                        }
                    }
                    else
                    {
                        ClearThongTinDocGia();
                    }
                }
            }
        }

        private void HienThiThongTinDocGia(DocGium docGia)
        {
            txtMaDocGia.Text = docGia.MaDocGia.ToString();
            txtTenDocGia.Text = docGia.TenDocGia;
            txtSDT.Text = docGia.Sdt ?? "Chưa có";
            txtEmail.Text = docGia.Email ?? "Chưa có";
            txtDiaChi.Text = docGia.DiaChi ?? "Chưa có";

            int tuoi = 0;
            if (docGia.NgaySinh.HasValue)
            {
                tuoi = DateTime.Now.Year - docGia.NgaySinh.Value.Year;
                if (DateTime.Now.DayOfYear < docGia.NgaySinh.Value.DayOfYear)
                    tuoi--;
            }

            lblThongTin.Text = $"{docGia.TenDocGia} ({tuoi} tuổi)";
            lblThongTin.ForeColor = Color.Blue;
        }

        private void btnTaoMoiDocGia_Click(object sender, EventArgs e)
        {
            var docGiaMoi = TaoMoiDocGia();
            if (docGiaMoi != null)
            {
                HienThiThongTinDocGia(docGiaMoi);
                txtTimSach.Focus();

                MessageBox.Show($"✓ Đã tạo độc giả mới thành công!\nMã: DG-{docGiaMoi.MaDocGia:0000}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearThongTinDocGia()
        {
            txtMaDocGia.Text = "";
            txtTenDocGia.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            lblThongTin.Text = "Chưa chọn độc giả";
            lblThongTin.ForeColor = Color.Gray;
        }

        private void btnTimSach_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimSach.Text.Trim();

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    MessageBox.Show("Vui lòng nhập tên sách, tác giả hoặc mã sách!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TimKiemSach(tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm sách: {ex.Message}");
            }
        }

        private void TimKiemSach(string tuKhoa)
        {
            using (var context = new ThuVienContext())
            {
                List<Models.Sach> ketQua = new List<Models.Sach>();

                if (int.TryParse(tuKhoa, out int maSach))
                {
                    var sach = context.Saches
                        .Include(s => s.MaTheLoaiNavigation)
                        .FirstOrDefault(s => s.MaSach == maSach);

                    if (sach != null) ketQua.Add(sach);
                }

                if (ketQua.Count == 0)
                {
                    ketQua = context.Saches
                        .Include(s => s.MaTheLoaiNavigation)
                        .Where(s => s.TenSach.Contains(tuKhoa) ||
                                   s.TacGia.Contains(tuKhoa))
                        .ToList();
                }

                if (ketQua.Count == 1)
                {
                    HienThiThongTinSach(ketQua[0]);
                }
                else if (ketQua.Count > 1)
                {
                    HienThiDialogChonSach(ketQua);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sách phù hợp!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearThongTinSach();
                }
            }
        }

        private void HienThiThongTinSach(Models.Sach sach)
        {
            txtMaSach.Text = sach.MaSach.ToString();
            txtTenSach.Text = sach.TenSach;
            txtTacGia.Text = sach.TacGia ?? "Không rõ";
            txtTheLoai.Text = sach.MaTheLoaiNavigation?.TenTheLoai ?? "Chưa phân loại";
            txtSoLuongCon.Text = sach.SoLuong.ToString();

            numSoLuongMuon.Value = 1;
            numSoLuongMuon.Maximum = sach.SoLuong;
            numSoLuongMuon.Minimum = 1;

            if (sach.SoLuong <= 0)
            {
                lblThongTinSach.Text = "✗ HẾT SÁCH";
                lblThongTinSach.ForeColor = Color.Red;
                btnThemVaoDanhSach.Enabled = false;
            }
            else if (sach.SoLuong <= 3)
            {
                lblThongTinSach.Text = $"⚠ Còn ít: {sach.SoLuong} cuốn";
                lblThongTinSach.ForeColor = Color.Orange;
                btnThemVaoDanhSach.Enabled = true;
            }
            else
            {
                lblThongTinSach.Text = $"✓ Còn nhiều: {sach.SoLuong} cuốn";
                lblThongTinSach.ForeColor = Color.Green;
                btnThemVaoDanhSach.Enabled = true;
            }
        }

        private void HienThiDialogChonSach(List<Models.Sach> danhSachSach)
        {
            Form formChon = new Form();
            formChon.Text = "Chọn sách (" + danhSachSach.Count + " kết quả)";
            formChon.Size = new Size(700, 400);
            formChon.StartPosition = FormStartPosition.CenterParent;

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã", DataPropertyName = "MaSach", Width = 50 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên sách", DataPropertyName = "TenSach", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tác giả", DataPropertyName = "TacGia", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thể loại", DataPropertyName = "MaTheLoaiNavigation.TenTheLoai", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "S.Lượng", DataPropertyName = "SoLuong", Width = 60 });

            dgv.DataSource = danhSachSach;

            Panel panelNut = new Panel();
            panelNut.Dock = DockStyle.Bottom;
            panelNut.Height = 50;

            Button btnChon = new Button();
            btnChon.Text = "Chọn";
            btnChon.Size = new Size(100, 30);
            btnChon.Location = new Point(280, 10);
            btnChon.Click += (s, ev) =>
            {
                if (dgv.CurrentRow != null)
                {
                    var sachChon = danhSachSach[dgv.CurrentRow.Index];
                    HienThiThongTinSach(sachChon);
                    formChon.DialogResult = DialogResult.OK;
                }
            };

            Button btnHuy = new Button();
            btnHuy.Text = "Hủy";
            btnHuy.Size = new Size(100, 30);
            btnHuy.Location = new Point(390, 10);
            btnHuy.Click += (s, ev) => formChon.Close();

            panelNut.Controls.Add(btnChon);
            panelNut.Controls.Add(btnHuy);

            dgv.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var sachChon = danhSachSach[e.RowIndex];
                    HienThiThongTinSach(sachChon);
                    formChon.DialogResult = DialogResult.OK;
                }
            };

            formChon.Controls.Add(dgv);
            formChon.Controls.Add(panelNut);
            formChon.ShowDialog();
        }

        private void ClearThongTinSach()
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtTheLoai.Text = "";
            txtSoLuongCon.Text = "";
            numSoLuongMuon.Value = 1;
            lblThongTinSach.Text = "Chưa chọn sách";
            lblThongTinSach.ForeColor = Color.Gray;
            btnThemVaoDanhSach.Enabled = true;
        }

        private void btnThemVaoDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSach.Text))
                {
                    MessageBox.Show("Vui lòng chọn sách trước!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimSach.Focus();
                    return;
                }

                if (!int.TryParse(txtMaSach.Text, out int maSach))
                {
                    MessageBox.Show("Mã sách không hợp lệ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int soLuongMuon = (int)numSoLuongMuon.Value;
                int soLuongCon = int.Parse(txtSoLuongCon.Text);

                if (soLuongMuon <= 0)
                {
                    MessageBox.Show("Số lượng mượn phải lớn hơn 0!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (soLuongMuon > soLuongCon)
                {
                    MessageBox.Show($"Không đủ sách! Chỉ còn {soLuongCon} cuốn.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var sachDaCo = danhSachMuon.FirstOrDefault(s => s.MaSach == maSach);
                if (sachDaCo != null)
                {
                    int tongSoLuong = sachDaCo.SoLuong + soLuongMuon;
                    if (tongSoLuong > soLuongCon)
                    {
                        MessageBox.Show($"Tổng số lượng mượn vượt quá số lượng còn ({soLuongCon})!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    sachDaCo.SoLuong = tongSoLuong;
                }
                else
                {
                    danhSachMuon.Add(new ChiTietMuon
                    {
                        MaSach = maSach,
                        TenSach = txtTenSach.Text,
                        SoLuong = soLuongMuon,
                        SoLuongCon = soLuongCon,
                        TacGia = txtTacGia.Text,
                        TheLoai = txtTheLoai.Text
                    });
                }

                LoadDataGridView();
                ClearThongTinSach();
                txtTimSach.Text = "";
                txtTimSach.Focus();

                lblThongBao.Text = $"Đã thêm sách vào danh sách ({danhSachMuon.Count} loại)";
                lblThongBao.ForeColor = Color.Green;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm sách: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataGridView()
        {
            dgvDanhSachMuon.Rows.Clear();
            int stt = 1;

            foreach (var item in danhSachMuon)
            {
                dgvDanhSachMuon.Rows.Add(
                    stt++,
                    item.MaSach,
                    item.TenSach,
                    item.TacGia,
                    item.TheLoai,
                    item.SoLuong,
                    item.SoLuongCon
                );
            }

            int tongSoLuongSach = danhSachMuon.Sum(s => s.SoLuong);
            lblTongSach.Text = $"Tổng: {danhSachMuon.Count} loại, {tongSoLuongSach} cuốn";
        }

        private void btnXoaKhoiDanhSach_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachMuon.CurrentRow != null)
            {
                int rowIndex = dgvDanhSachMuon.CurrentRow.Index;
                if (rowIndex >= 0 && rowIndex < danhSachMuon.Count)
                {
                    string tenSach = danhSachMuon[rowIndex].TenSach;

                    var result = MessageBox.Show($"Xóa sách '{tenSach}' khỏi danh sách?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        danhSachMuon.RemoveAt(rowIndex);
                        LoadDataGridView();
                        lblThongBao.Text = "Đã xóa sách khỏi danh sách";
                        lblThongBao.ForeColor = Color.Orange;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void numSoNgayMuon_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayTraDuKien.Value = dtpNgayMuon.Value.AddDays((int)numSoNgayMuon.Value);
        }

        private void dtpNgayMuon_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayTraDuKien.Value = dtpNgayMuon.Value.AddDays((int)numSoNgayMuon.Value);
        }

        private void btnXacNhanMuon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieuHopLe()) return;
                if (!HienThiXacNhan()) return;
                LuuPhieuMuon();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tạo phiếu mượn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraDuLieuHopLe()
        {
            if (!int.TryParse(txtMaDocGia.Text, out int maDocGia) || maDocGia <= 0)
            {
                MessageBox.Show("Vui lòng chọn độc giả hợp lệ!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDocGia.Focus();
                return false;
            }

            if (danhSachMuon.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 sách vào danh sách mượn!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTimSach.Focus();
                return false;
            }

            if (cbNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên xử lý!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dtpNgayTraDuKien.Value <= dtpNgayMuon.Value)
            {
                MessageBox.Show("Ngày trả dự kiến phải sau ngày mượn!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayMuon.Focus();
                return false;
            }

            using (var context = new ThuVienContext())
            {
                foreach (var item in danhSachMuon)
                {
                    var sach = context.Saches.Find(item.MaSach);
                    if (sach == null)
                    {
                        MessageBox.Show($"Sách '{item.TenSach}' không tồn tại!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (sach.SoLuong < item.SoLuong)
                    {
                        MessageBox.Show($"Sách '{item.TenSach}' chỉ còn {sach.SoLuong} cuốn!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        private bool HienThiXacNhan()
        {
            string thongTin =
                $"ĐỘC GIẢ: {txtTenDocGia.Text}\n" +
                $"SỐ LƯỢNG SÁCH: {danhSachMuon.Count} loại ({danhSachMuon.Sum(s => s.SoLuong)} cuốn)\n" +
                $"NGÀY MƯỢN: {dtpNgayMuon.Value:dd/MM/yyyy}\n" +
                $"HẠN TRẢ: {dtpNgayTraDuKien.Value:dd/MM/yyyy}\n" +
                $"NHÂN VIÊN: {cbNhanVien.Text}\n\n" +
                $"Bạn có chắc chắn muốn tạo phiếu mượn?";

            var result = MessageBox.Show(thongTin,
                "XÁC NHẬN MƯỢN SÁCH",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        private void LuuPhieuMuon()
        {
            using (var context = new ThuVienContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var phieuMuon = new PhieuMuon
                    {
                        MaDocGia = int.Parse(txtMaDocGia.Text),
                        MaNhanVien = (int)cbNhanVien.SelectedValue,
                        NgayMuon = dtpNgayMuon.Value.Date,
                        NgayTraDuKien = dtpNgayTraDuKien.Value.Date,
                        TrangThai = "Đang mượn"
                    };

                    context.PhieuMuons.Add(phieuMuon);
                    context.SaveChanges();

                    foreach (var item in danhSachMuon)
                    {
                        var chiTiet = new ChiTietPhieuMuon
                        {
                            MaPhieu = phieuMuon.MaPhieu,
                            MaSach = item.MaSach,
                            SoLuong = item.SoLuong
                        };
                        context.ChiTietPhieuMuons.Add(chiTiet);

                        var sach = context.Saches.Find(item.MaSach);
                        sach.SoLuong -= item.SoLuong;
                    }

                    context.SaveChanges();
                    transaction.Commit();

                    HienThiThanhCong(phieuMuon.MaPhieu);
                    ResetForm();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void HienThiThanhCong(int maPhieu)
        {
            string thongTinPhieu =
                $"✅ TẠO PHIẾU MƯỢN THÀNH CÔNG!\n\n" +
                $"Mã phiếu: PM-{maPhieu:0000}\n" +
                $"Ngày mượn: {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                $"Hạn trả: {dtpNgayTraDuKien.Value:dd/MM/yyyy}\n" +
                $"Số sách: {danhSachMuon.Sum(s => s.SoLuong)} cuốn\n\n" +
                $"Lưu ý: Độc giả cần trả sách đúng hạn!";

            MessageBox.Show(thongTinPhieu,
                "THÀNH CÔNG",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ResetForm()
        {
            ClearThongTinDocGia();
            danhSachMuon.Clear();
            LoadDataGridView();
            ClearThongTinSach();
            txtTimSach.Text = "";

            dtpNgayMuon.Value = DateTime.Now.Date;
            numSoNgayMuon.Value = soNgayMuonMacDinh;
            dtpNgayTraDuKien.Value = DateTime.Now.Date.AddDays(soNgayMuonMacDinh);

            txtMaDocGia.Focus();

            lblThongBao.Text = "Phiếu mượn đã được lưu. Bạn có thể tạo phiếu mới.";
            lblThongBao.ForeColor = Color.Blue;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (danhSachMuon.Count > 0)
            {
                var result = MessageBox.Show(
                    "Bạn có dữ liệu chưa lưu. Bạn có chắc chắn muốn hủy?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }

            this.Close();
        }

        private void txtTimSach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnTimSach.PerformClick();
                e.Handled = true;
            }
        }

        private void txtMaDocGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnTimDocGia.PerformClick();
                e.Handled = true;
            }
        }

        private void dgvDanhSachMuon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSachMuon.CurrentRow != null && dgvDanhSachMuon.CurrentRow.Index < danhSachMuon.Count)
            {
                var sach = danhSachMuon[dgvDanhSachMuon.CurrentRow.Index];
                lblThongTinSach.Text = $"Đang chọn: {sach.TenSach} ({sach.SoLuong} cuốn)";
            }
        }
    }
}
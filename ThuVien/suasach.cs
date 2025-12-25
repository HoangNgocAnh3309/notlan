using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThuVien.Models;

namespace ThuVien
{
    public partial class suasach : Form
    {
        // THÊM CÁC THUỘC TÍNH NÀY
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaSach { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TenSach { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TacGia { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TheLoai { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NamXuatBan { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SoLuong { get; set; }

        public suasach()
        {
            InitializeComponent();
        }

        // THÊM CONSTRUCTOR CÓ THAM SỐ
        public suasach(int maSach)
        {
            InitializeComponent();
            MaSach = maSach;
            LoadThongTinSach();
            LoadTheLoai();
        }

        // THÊM PHƯƠNG THỨC LOAD THÔNG TIN SÁCH
        private void LoadThongTinSach()
        {
            try
            {
                using (var context = new ThuVienContext())
                {
                    var sach = context.Saches
                        .Include(s => s.MaTheLoaiNavigation)
                        .FirstOrDefault(s => s.MaSach == MaSach);

                    if (sach != null)
                    {
                        // Hiển thị thông tin lên form
                        txtTenSach.Text = sach.TenSach;
                        txtTacGia.Text = sach.TacGia ?? "";
                        txtNamXuatBan.Text = sach.NamXuatBan?.ToString() ?? "";
                        txtSoLuong.Text = sach.SoLuong.ToString();

                        // Lưu vào thuộc tính
                        TenSach = sach.TenSach;
                        TacGia = sach.TacGia ?? "";
                        TheLoai = sach.MaTheLoaiNavigation?.TenTheLoai ?? "";
                        NamXuatBan = sach.NamXuatBan ?? 0;
                        SoLuong = sach.SoLuong;

                        this.Text = $"Sửa sách: {sach.TenSach}";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sách!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                this.Close();
            }
        }

        // THÊM PHƯƠNG THỨC LOAD THỂ LOẠI
        private void LoadTheLoai()
        {
            try
            {
                using (var context = new ThuVienContext())
                {
                    var theLoais = context.TheLoais.ToList();
                    cbTheLoai.DataSource = theLoais;
                    cbTheLoai.DisplayMember = "TenTheLoai";
                    cbTheLoai.ValueMember = "MaTheLoai";

                    // Chọn thể loại của sách hiện tại
                    if (!string.IsNullOrEmpty(TheLoai))
                    {
                        var selected = theLoais.FirstOrDefault(tl => tl.TenTheLoai == TheLoai);
                        if (selected != null)
                        {
                            cbTheLoai.SelectedValue = selected.MaTheLoai;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load thể loại: {ex.Message}");
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

        // THÊM NÚT LƯU
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (string.IsNullOrWhiteSpace(txtTenSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sách!");
                    txtTenSach.Focus();
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ!");
                    txtSoLuong.Focus();
                    return;
                }

                using (var context = new ThuVienContext())
                {
                    var sach = context.Saches.Find(MaSach);
                    if (sach != null)
                    {
                        // Cập nhật thông tin
                        sach.TenSach = txtTenSach.Text.Trim();
                        sach.TacGia = txtTacGia.Text.Trim();

                        if (int.TryParse(txtNamXuatBan.Text, out int namXB))
                        {
                            sach.NamXuatBan = namXB;
                        }

                        sach.SoLuong = soLuong;

                        // Cập nhật thể loại
                        if (cbTheLoai.SelectedValue != null &&
                            int.TryParse(cbTheLoai.SelectedValue.ToString(), out int maTheLoai))
                        {
                            sach.MaTheLoai = maTheLoai;
                        }

                        context.SaveChanges();
                        MessageBox.Show("Cập nhật sách thành công!");

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        // THÊM KIỂM TRA NHẬP SỐ
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNamXB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // THÊM SỰ KIỆN LOAD FORM
        private void suasach_Load(object sender, EventArgs e)
        {
            // Nếu không dùng constructor có tham số
            if (MaSach > 0)
            {
                LoadThongTinSach();
                LoadTheLoai();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (string.IsNullOrWhiteSpace(txtTenSach.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sách!");
                    txtTenSach.Focus();
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ!");
                    txtSoLuong.Focus();
                    return;
                }

                using (var context = new ThuVienContext())
                {
                    var sach = context.Saches.Find(MaSach);
                    if (sach != null)
                    {
                        // Cập nhật thông tin
                        sach.TenSach = txtTenSach.Text.Trim();
                        sach.TacGia = txtTacGia.Text.Trim();

                        if (int.TryParse(txtNamXuatBan.Text, out int namXB))
                        {
                            sach.NamXuatBan = namXB;
                        }

                        sach.SoLuong = soLuong;

                        // Cập nhật thể loại
                        if (cbTheLoai.SelectedValue != null &&
                            int.TryParse(cbTheLoai.SelectedValue.ToString(), out int maTheLoai))
                        {
                            sach.MaTheLoai = maTheLoai;
                        }

                        context.SaveChanges();
                        MessageBox.Show("Cập nhật sách thành công!");

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
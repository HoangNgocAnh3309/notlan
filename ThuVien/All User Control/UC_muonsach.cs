using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ThuVien.All_User_Control
{
    public partial class UC_muonsach : UserControl
    {
        public UC_muonsach()
        {
            InitializeComponent();
        }

        // Tên độc giả
        public void SetTenDocGia(string value) => lblTenDocGia.Text = value;
        public string GetTenDocGia() => lblTenDocGia.Text;

        // Mã độc giả
        public void SetMaDocGia(int value) => lblMaDocGia.Text = $"MĐG: {value}";
        public string GetMaDocGia() => lblMaDocGia.Text;

        // Tên sách
        public void SetTenSach(string value) => lblTenSach.Text = value;
        public string GetTenSach() => lblTenSach.Text;

        // Ngày mượn
        public void SetNgayMuon(DateTime value) => lblNgayMuon.Text = value.ToString("dd/MM/yyyy");
        public string GetNgayMuon() => lblNgayMuon.Text;

        // Ngày trả dự kiến
        public void SetNgayTraDuKien(DateTime value) => lblNgayTraDuKien.Text = value.ToString("dd/MM/yyyy");
        public string GetNgayTraDuKien() => lblNgayTraDuKien.Text;

        // Trạng thái
        public void SetTrangThai(string trangThai)
        {
            lblTrangThai.Text = trangThai;

            // Đặt màu theo trạng thái VÀ ẩn/hiện nút
            if (trangThai == "Đang mượn")
            {
                lblTrangThai.ForeColor = Color.Blue;
                lblTrangThai.BackColor = Color.FromArgb(230, 240, 255);
                guna2Button3.Visible = true; // Hiện nút
            }
            else if (trangThai == "Đã trả")
            {
                lblTrangThai.ForeColor = Color.Green;
                lblTrangThai.BackColor = Color.FromArgb(230, 255, 240);
                guna2Button3.Visible = false; // Ẩn nút
            }
            else if (trangThai == "Quá hạn")
            {
                lblTrangThai.ForeColor = Color.Red;
                lblTrangThai.BackColor = Color.FromArgb(255, 230, 230);
                guna2Button3.Visible = true; // Hiện nút
            }
            else
            {
                lblTrangThai.ForeColor = Color.Gray;
                lblTrangThai.BackColor = Color.FromArgb(240, 240, 240);
                guna2Button3.Visible = true; // Mặc định hiện nút
            }
        }

        public string GetTrangThai() => lblTrangThai.Text;

        // Mã phiếu
        private int _maPhieu;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaPhieu
        {
            get => _maPhieu;
            set => _maPhieu = value;
        }

        // Event xử lý trả sách
        public event Action<int> TraSachEvent;

        // Khi click nút guna2Button3 (nút trả sách)
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Hiển thị xác nhận
            var result = MessageBox.Show(
                $"Xác nhận trả sách cho độc giả '{lblTenDocGia.Text}'?",
                "Xác nhận trả sách",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Gọi event trả sách, truyền mã phiếu
                TraSachEvent?.Invoke(MaPhieu);
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            // Hiển thị xác nhận
            var result = MessageBox.Show(
                $"Xác nhận trả sách cho độc giả '{lblTenDocGia.Text}'?",
                "Xác nhận trả sách",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Gọi event trả sách, truyền mã phiếu
                TraSachEvent?.Invoke(MaPhieu);
            }
        }
    }
}
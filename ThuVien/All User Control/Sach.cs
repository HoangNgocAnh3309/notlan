using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ThuVien.All_User_Control
{
    public partial class Sach : UserControl
    {
        public Sach()
        {
            InitializeComponent();
        }

        // Tên sách
        public void SetTenSach(string value) => lblTenSach.Text = value;
        public string GetTenSach() => lblTenSach.Text;

        // =========================
        // Tác giả
        public void SetTacGia(string value) => label8.Text = value;
        public string GetTacGia() => label8.Text;

        public void SetTheLoai(string value) => lblTheLoai.Text = value;
        public string GetTheLoai() => lblTheLoai.Text;

        // Năm xuất bản
        public void SetNamXB(string value) => lblNam.Text = value;
        public string GetNamXB() => lblNam.Text;

        // Trạng thái
        public void SetTrangThaiTheoSoLuong(int soLuongConLai)
        {
            string trangThai;
            if (soLuongConLai > 0)
                trangThai = "Sẵn sàng(" + soLuongConLai + ")";
            else
                trangThai = "Tạm hết";

            lblTrangThai.Text = trangThai;

            if (trangThai == "Tạm hết")
            {
                lblTrangThai.ForeColor = Color.Orange;
                lblTrangThai.FillColor = Color.FromArgb(50, Color.Orange);
            }
            else // Sẵn sàng
            {
                lblTrangThai.ForeColor = Color.Green;
                lblTrangThai.FillColor = Color.FromArgb(50, Color.Green);
            }
        }

        public string GetTrangThai() => lblTrangThai.Text;

        private void lblTacGia_Paint(object sender, PaintEventArgs e)
        {
        }

        // Event xóa sách
        public event Action<int> XoaSachEvent;

        // THUỘC TÍNH MaSach - SỬA THEO CÁCH NÀY
        private int _maSach;

        [Browsable(false)]  // Ẩn khỏi Properties window
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] // Quan trọng
        public int MaSach
        {
            get => _maSach;
            set => _maSach = value;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                XoaSachEvent?.Invoke(MaSach);
            }
        }
    }
}
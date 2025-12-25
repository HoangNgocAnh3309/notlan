using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ThuVien.Models;

namespace ThuVien.All_User_Control
{
    public partial class UC_NhanVien : UserControl
    {
        public event EventHandler OnXemChiTiet;

        private NhanVien _nhanVien;

        // Property cho Designer (không cần serialization)
        [Browsable(false)] // Ẩn khỏi Property Grid
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NhanVien NhanVienInfo
        {
            get { return _nhanVien; }
            set
            {
                _nhanVien = value;
                if (!DesignMode) // Chỉ chạy khi không ở chế độ Design
                {
                    HienThiThongTin();
                }
            }
        }

        public UC_NhanVien()
        {
            InitializeComponent();
            if (!DesignMode) // Chỉ đăng ký sự kiện khi không ở chế độ Design
            {
                btnXemchitietvalichlam.Click += BtnXemChiTiet_Click;
            }
        }

        public UC_NhanVien(NhanVien nv) : this()
        {
            _nhanVien = nv;
            if (!DesignMode)
            {
                HienThiThongTin();
            }
        }

        private void HienThiThongTin()
        {
            if (_nhanVien != null)
            {
                // Hiển thị thông tin
                lblTenNhanVien.Text = _nhanVien.TenNhanVien;
                lblChucVu.Text = !string.IsNullOrEmpty(_nhanVien.ChucVu)
                    ? _nhanVien.ChucVu.ToUpper()
                    : "NHÂN VIÊN";

                // Đặt màu theo chức vụ
                DatMauChucVu();
            }
        }

        private void DatMauChucVu()
        {
            if (string.IsNullOrEmpty(_nhanVien?.ChucVu))
            {
                lblChucVu.ForeColor = Color.Gray;
                return;
            }

            string chucVu = _nhanVien.ChucVu.ToLower();

            if (chucVu.Contains("quản lý") || chucVu.Contains("admin"))
            {
                lblChucVu.ForeColor = Color.FromArgb(220, 0, 0);
                lblTenNhanVien.ForeColor = Color.FromArgb(220, 0, 0);
            }
            else if (chucVu.Contains("trưởng"))
            {
                lblChucVu.ForeColor = Color.FromArgb(0, 100, 200);
                lblTenNhanVien.ForeColor = Color.FromArgb(0, 100, 200);
            }
            else
            {
                lblChucVu.ForeColor = Color.FromArgb(0, 150, 136);
                lblTenNhanVien.ForeColor = Color.FromArgb(0, 150, 136);
            }
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            OnXemChiTiet?.Invoke(this, EventArgs.Empty);
        }

        // Phương thức để set dữ liệu trực tiếp (không dùng property)
        public void SetThongTin(NhanVien nv)
        {
            _nhanVien = nv;
            HienThiThongTin();
        }

        // Hoặc overload
        public void SetThongTin(string ten, string chucVu)
        {
            lblTenNhanVien.Text = ten;
            lblChucVu.Text = !string.IsNullOrEmpty(chucVu)
                ? chucVu.ToUpper()
                : "NHÂN VIÊN";

            // Cập nhật màu
            if (!string.IsNullOrEmpty(chucVu))
            {
                string cv = chucVu.ToLower();
                if (cv.Contains("quản lý") || cv.Contains("admin"))
                {
                    lblChucVu.ForeColor = Color.FromArgb(220, 0, 0);
                    lblTenNhanVien.ForeColor = Color.FromArgb(220, 0, 0);
                }
                else if (cv.Contains("trưởng"))
                {
                    lblChucVu.ForeColor = Color.FromArgb(0, 100, 200);
                    lblTenNhanVien.ForeColor = Color.FromArgb(0, 100, 200);
                }
                else
                {
                    lblChucVu.ForeColor = Color.FromArgb(0, 150, 136);
                    lblTenNhanVien.ForeColor = Color.FromArgb(0, 150, 136);
                }
            }
        }

        // Mouse hover effect
        private void UC_NhanVien_MouseEnter(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.Cursor = Cursors.Hand;
                guna2Panel2.ShadowDecoration.Enabled = true;
                guna2Panel2.ShadowDecoration.Depth = 10;
            }
        }

        private void UC_NhanVien_MouseLeave(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.Cursor = Cursors.Default;
                guna2Panel2.ShadowDecoration.Enabled = false;
            }
        }
    }
}
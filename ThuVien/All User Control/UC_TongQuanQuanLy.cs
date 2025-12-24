using Guna.Charts.WinForms;
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
namespace ThuVien.All_User_Control
{
    public partial class UC_TongQuanQuanLy : UserControl
    {
        public UC_TongQuanQuanLy()
        {
            InitializeComponent();
            LoadThongKeSach();
        }
        public void LoadThongKeSach()
        {
            using (var db = new ThuVienContext())
            {
                // =========================
                // 1. TỔNG SỐ SÁCH
                // =========================
                int tongSach = db.Saches.Sum(s => s.SoLuong);

                // =========================
                // 2. SÁCH THÊM TRONG TUẦN
                // =========================
                DateTime today = DateTime.Today;

                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime startOfWeek = today.AddDays(-diff);
                DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

                int countNewBooks = db.Saches
                    .Count(s => s.NgayThem >= startOfWeek && s.NgayThem <= endOfWeek);

                // =========================
                // 3. SÁCH ĐANG MƯỢN
                // =========================
                int soSachDangMuon = db.ChiTietPhieuMuons
                    .Where(ct => ct.MaPhieuNavigation.TrangThai == "Đang mượn")
                    .Select(ct => (int?)ct.SoLuong)
                    .Sum() ?? 0;

                // =========================
                // 4. SÁCH QUÁ HẠN
                // =========================
                DateTime now = DateTime.Now;

                int soSachQuaHan = db.ChiTietPhieuMuons
                    .Where(ct =>
                        ct.MaPhieuNavigation.TrangThai == "Đang mượn" &&
                        ct.MaPhieuNavigation.NgayTraDuKien < now)
                    .Select(ct => (int?)ct.SoLuong)
                    .Sum() ?? 0;

                // =========================
                // 5. SÁCH SẴN SÀNG (TÍNH ĐỘNG)
                // =========================
                int soSachSanSang = tongSach - (soSachDangMuon + soSachQuaHan);

                if (soSachSanSang < 0)
                    soSachSanSang = 0; // phòng dữ liệu lỗi

                // =========================
                // 6. GÁN LÊN UI
                // =========================
                lblTongSach.Text = tongSach.ToString();
                lbl_sosachtheotuan.Text = countNewBooks.ToString();
                lblSoSachMuon.Text = soSachDangMuon.ToString();
                lblQuaHan.Text = soSachQuaHan.ToString();
                // nhớ thêm label này
            }

        }
        private void UC_TongQuanQuanLy_Load(object sender, EventArgs e)
        {
            gunaChartMuonTra.YAxes.GridLines.Display = true;
            gunaChartMuonTra.XAxes.GridLines.Display = false;
            gunaChartMuonTra.Legend.Position = Guna.Charts.WinForms.LegendPosition.Top;
            var datasetMuon = new GunaSplineDataset();
            datasetMuon.Label = "Mượn";
            datasetMuon.BorderColor = Color.FromArgb(100, 100, 255);
            datasetMuon.PointStyle = PointStyle.Circle;
            datasetMuon.PointRadius = 5;
            datasetMuon.PointFillColors.Add(Color.White);
            datasetMuon.PointBorderColors.Add(Color.FromArgb(100, 100, 255));
            datasetMuon.PointBorderWidth = 2;
            datasetMuon.DataPoints.Add("T2", 40);
            datasetMuon.DataPoints.Add("T3", 30);
            datasetMuon.DataPoints.Add("T4", 20);
            datasetMuon.DataPoints.Add("T5", 28);
            datasetMuon.DataPoints.Add("T6", 18);
            datasetMuon.DataPoints.Add("T7", 25);
            datasetMuon.DataPoints.Add("CN", 35);
            var datasetTra = new GunaSplineDataset();
            datasetTra.Label = "Trả";
            datasetTra.BorderColor = Color.FromArgb(0, 200, 150);
            datasetTra.PointStyle = PointStyle.Circle;
            datasetTra.PointRadius = 5;
            datasetTra.PointFillColors.Add(Color.White);
            datasetTra.PointBorderColors.Add(Color.FromArgb(0, 200, 150));
            datasetTra.PointBorderWidth = 2;
            datasetTra.DataPoints.Add("T2", 25);
            datasetTra.DataPoints.Add("T3", 14);
            datasetTra.DataPoints.Add("T4", 60);
            datasetTra.DataPoints.Add("T5", 40);
            datasetTra.DataPoints.Add("T6", 50);
            datasetTra.DataPoints.Add("T7", 40);
            datasetTra.DataPoints.Add("CN", 45);
            gunaChartMuonTra.Datasets.Clear();
            gunaChartMuonTra.Datasets.Add(datasetMuon);
            gunaChartMuonTra.Datasets.Add(datasetTra);
            gunaChartMuonTra.Update();






            gunaChartDanhMuc.Legend.Display = false;
            gunaChartDanhMuc.YAxes.GridLines.Display = false;
            gunaChartDanhMuc.XAxes.GridLines.Display = true;
            gunaChartDanhMuc.XAxes.Display = false;
            var datasetBar = new GunaHorizontalBarDataset();
            Color barColor = Color.FromArgb(100, 100, 255);
            datasetBar.FillColors.Add(barColor);
            datasetBar.CornerRadius = 5;
            datasetBar.DataPoints.Add("Khoa học", 45);
            datasetBar.DataPoints.Add("Lịch sử", 60);
            datasetBar.DataPoints.Add("Kinh tế", 85);
            datasetBar.DataPoints.Add("CNTT", 85);
            datasetBar.DataPoints.Add("Văn học", 100);
            gunaChartDanhMuc.Datasets.Clear();
            gunaChartDanhMuc.Datasets.Add(datasetBar);
            gunaChartDanhMuc.Update();
        }
    }
}

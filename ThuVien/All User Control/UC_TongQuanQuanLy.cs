using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThuVien.All_User_Control
{
    public partial class UC_TongQuanQuanLy : UserControl
    {
        public UC_TongQuanQuanLy()
        {
            InitializeComponent();
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

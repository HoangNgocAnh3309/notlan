using System;
using System.Collections.Generic;

namespace ThuVien.Models;

public partial class PhieuMuon
{
    public int MaPhieu { get; set; }

    public int MaDocGia { get; set; }

    public int MaNhanVien { get; set; }

    public DateTime NgayMuon { get; set; }
    public DateTime NgayTraDuKien { get; set; }
    public DateTime? NgayTraThucTe { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new List<ChiTietPhieuMuon>();

    public virtual DocGium MaDocGiaNavigation { get; set; } = null!;

    public virtual NhanVien MaNhanVienNavigation { get; set; } = null!;
}

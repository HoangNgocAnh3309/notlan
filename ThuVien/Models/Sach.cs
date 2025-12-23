using System;
using System.Collections.Generic;

namespace ThuVien.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    public string TenSach { get; set; } = null!;

    public string? TacGia { get; set; }

    public string? NhaXuatBan { get; set; }

    public int? NamXuatBan { get; set; }

    public int SoLuong { get; set; }

    public int? MaTheLoai { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new List<ChiTietPhieuMuon>();

    public virtual TheLoai? MaTheLoaiNavigation { get; set; }
}

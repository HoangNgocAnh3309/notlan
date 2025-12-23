using System;
using System.Collections.Generic;

namespace ThuVien.Models;

public partial class DocGium
{
    public int MaDocGia { get; set; }

    public string TenDocGia { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();
}

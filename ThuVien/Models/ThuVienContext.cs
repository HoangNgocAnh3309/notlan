using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ThuVien.Models;

public partial class ThuVienContext : DbContext
{
    public ThuVienContext()
    {
    }

    public ThuVienContext(DbContextOptions<ThuVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

    public virtual DbSet<DocGium> DocGia { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=thu_vien;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhieuMuon>(entity =>
        {
            entity.HasKey(e => new { e.MaPhieu, e.MaSach }).HasName("PK__chi_tiet__6147602686637262");

            entity.ToTable("chi_tiet_phieu_muon");

            entity.Property(e => e.MaPhieu).HasColumnName("ma_phieu");
            entity.Property(e => e.MaSach).HasColumnName("ma_sach");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");

            entity.HasOne(d => d.MaPhieuNavigation).WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(d => d.MaPhieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ctpm_phieu");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(d => d.MaSach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ctpm_sach");
        });

        modelBuilder.Entity<DocGium>(entity =>
        {
            entity.HasKey(e => e.MaDocGia).HasName("PK__doc_gia__F91F4A3F717F0F51");

            entity.ToTable("doc_gia");

            entity.Property(e => e.MaDocGia).HasColumnName("ma_doc_gia");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("dia_chi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .HasColumnName("gioi_tinh");
            entity.Property(e => e.NgaySinh).HasColumnName("ngay_sinh");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("sdt");
            entity.Property(e => e.TenDocGia)
                .HasMaxLength(150)
                .HasColumnName("ten_doc_gia");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__nhan_vie__6781B7B9CDB56532");

            entity.ToTable("nhan_vien");

            entity.HasIndex(e => e.TaiKhoan, "UQ__nhan_vie__ABA0D027665823F9").IsUnique();

            entity.Property(e => e.MaNhanVien).HasColumnName("ma_nhan_vien");
            entity.Property(e => e.ChucVu)
                .HasMaxLength(100)
                .HasColumnName("chuc_vu");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .HasColumnName("mat_khau");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("sdt");
            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(50)
                .HasColumnName("tai_khoan");
            entity.Property(e => e.TenNhanVien)
                .HasMaxLength(150)
                .HasColumnName("ten_nhan_vien");
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.MaPhieu).HasName("PK__phieu_mu__11D0F07A791A3496");

            entity.ToTable("phieu_muon");

            entity.Property(e => e.MaPhieu).HasColumnName("ma_phieu");
            entity.Property(e => e.MaDocGia).HasColumnName("ma_doc_gia");
            entity.Property(e => e.MaNhanVien).HasColumnName("ma_nhan_vien");
            entity.Property(e => e.NgayMuon).HasColumnName("ngay_muon");
            entity.Property(e => e.NgayTraDuKien).HasColumnName("ngay_tra_du_kien");
            entity.Property(e => e.NgayTraThucTe).HasColumnName("ngay_tra_thuc_te");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đang mượn")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaDocGiaNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.MaDocGia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pm_doc_gia");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.PhieuMuons)
                .HasForeignKey(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pm_nhan_vien");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach).HasName("PK__sach__097905C6A5BB11C4");

            entity.ToTable("sach");

            entity.Property(e => e.MaSach).HasColumnName("ma_sach");
            entity.Property(e => e.MaTheLoai).HasColumnName("ma_the_loai");
            entity.Property(e => e.NamXuatBan).HasColumnName("nam_xuat_ban");
            entity.Property(e => e.NhaXuatBan)
                .HasMaxLength(150)
                .HasColumnName("nha_xuat_ban");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");
            entity.Property(e => e.TacGia)
                .HasMaxLength(150)
                .HasColumnName("tac_gia");
            entity.Property(e => e.TenSach)
                .HasMaxLength(200)
                .HasColumnName("ten_sach");


            // <-- thêm dòng này
            entity.Property(e => e.NgayThem)
                .HasColumnName("ngay_them")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.MaTheLoaiNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaTheLoai)
                .HasConstraintName("FK_sach_the_loai");
        });


        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.HasKey(e => e.MaTheLoai).HasName("PK__the_loai__489AA0F3FFFBF831");

            entity.ToTable("the_loai");

            entity.Property(e => e.MaTheLoai).HasColumnName("ma_the_loai");
            entity.Property(e => e.TenTheLoai)
                .HasMaxLength(100)
                .HasColumnName("ten_the_loai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

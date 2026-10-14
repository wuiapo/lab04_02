using Microsoft.EntityFrameworkCore;

namespace lab04_01.Models
{
    public class QuanLySinhVienDbContext : DbContext
    {
        public DbSet<SinhVien> SinhViens { get; set; } = null!;
        public DbSet<Khoa> Khoas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=QuanLySinhVienDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Không cho xóa Khoa khi còn sinh viên (mặc định EF sẽ xóa luôn sinh viên)
            modelBuilder.Entity<SinhVien>()
                .HasOne(sv => sv.Khoa)
                .WithMany()
                .HasForeignKey(sv => sv.KhoaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mã SV không được trùng
            modelBuilder.Entity<SinhVien>()
                .HasIndex(sv => sv.MaSV)
                .IsUnique();
        }
    }
}
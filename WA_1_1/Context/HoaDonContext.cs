using Microsoft.EntityFrameworkCore;
using WA_1_1.Entitites;

namespace WA_1_1.Context
{
    public class HoaDonContext : DbContext
    {
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=DESKTOP-DP392M4;Database = WA_1_1;Integrated Security = true;TrustServerCertificate=True");
        }
    }
}

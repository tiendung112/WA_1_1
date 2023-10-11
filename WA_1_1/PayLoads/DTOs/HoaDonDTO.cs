using System.ComponentModel.DataAnnotations.Schema;
using WA_1_1.Entitites;

namespace WA_1_1.PayLoads.DTOs
{
    public class HoaDonDTO
    {
        public int KhachHangID { get; set; }
        public string TenHoaDon { get; set; }

        public string? MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhap { get; set; }
        public string GhiChu { get; set; }
        public double? TongTien { get; set; }
        public IQueryable<ChiTietHoaDonDTO> chiTietHoaDonDTOs { get; set; }
    }
}

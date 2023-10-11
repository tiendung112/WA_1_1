using WA_1_1.Entitites;

namespace WA_1_1.PayLoads.DTOs
{
    public class ChiTietHoaDonDTO
    {
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
        public double? ThanhTien { get; set; }
    }
}

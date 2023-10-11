using WA_1_1.Entitites;

namespace WA_1_1.PayLoads.Requests
{
    public class ThemChiTietHoaDonRequest
    {
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
        public List<ThemChiTietHoaDonRequest> themChiTietHoaDonRequests { get; set; }
    }
}

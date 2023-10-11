namespace WA_1_1.Entitites
{
    public class ChiTietHoaDon
    {
        public int ChiTietHoaDonID { get; set; }
        public int HoaDonID { get; set; }
        public HoaDon? HoaDon { get; set; }
        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
        public double? ThanhTien { get; set; }
    }
}

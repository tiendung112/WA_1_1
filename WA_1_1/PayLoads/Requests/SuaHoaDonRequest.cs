namespace WA_1_1.PayLoads.Requests
{
    public class SuaHoaDonRequest
    {
        public int HoaDonID { get; set; }
        public int KhachHangID { get; set; }

        public string TenHoaDon { get; set; }

        public DateTime? ThoiGianCapNhap { get; set; }
        public string GhiChu { get; set; }
        public List<SuaChiTietHoaDonRequest> suaChiTietHoaDonRequests { get; set; }
    }
}

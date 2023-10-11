using System.ComponentModel.DataAnnotations.Schema;

namespace WA_1_1.Entitites
{
    public class HoaDon
    {
        public int HoaDonID { get; set; }
        public int KhachHangID { get; set; }
        public KhachHang? KhachHang { get; set; }
        public string TenHoaDon { get; set; }

        public string? MaGiaoDich { get; set; }
        [Column(TypeName = "date")]
        public DateTime ThoiGianTao { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ThoiGianCapNhap { get; set; }

        public string GhiChu { get; set; }
        public double? TongTien { get; set; }


        // [NotMapped] //dùng để đếm hoá đơn trong ngày
        //public int coutHoaDon { get; set; }
        public IList<ChiTietHoaDon>? chiTietHoaDon { get; set; }
    }
}

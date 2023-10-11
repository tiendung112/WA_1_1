using System.ComponentModel.DataAnnotations.Schema;

namespace WA_1_1.Entitites
{
    public class SanPham
    {
        public int SanPhamID { get; set; }
        public int LoaiSanPhamID { get; set; }
        public LoaiSanPham LoaiSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double GiaThanh { get; set; }

        public string MoTa { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayHetHan { get; set; }
        public string KiHieuSanPham { get; set; }
        public IList<ChiTietHoaDon> ChiTietHoaDon { get; set; }
    }
}

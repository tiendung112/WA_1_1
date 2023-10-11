using System.ComponentModel.DataAnnotations.Schema;

namespace WA_1_1.Entitites
{
    public class KhachHang
    {
        public int KhachHangID { get; set; }
        public string HoTen { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        public string SDT { get; set; }
        public IList<HoaDon> hoaDon { get; set; }
    }
}

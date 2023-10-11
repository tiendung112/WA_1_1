using System.ComponentModel.DataAnnotations.Schema;
using WA_1_1.Entitites;

namespace WA_1_1.PayLoads.Requests
{
    public class ThemHoaDonRequest
    {
        public int KhachHangID { get; set; }
        public string TenHoaDon { get; set; }
        public string GhiChu { get; set; }
        public List<ThemChiTietHoaDonRequest> themChiTietHoaDonRequests { get; set; }
    }
}

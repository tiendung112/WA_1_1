using WA_1_1.Context;
using WA_1_1.Entitites;
using WA_1_1.PayLoads.DTOs;

namespace WA_1_1.PayLoads.Converters
{
    public class HoaDonConverter
    {
        private readonly HoaDonContext context;
        private readonly ChiTietConverter chiTietConverter;
        public HoaDonConverter()
        {
            context = new HoaDonContext();
            chiTietConverter = new ChiTietConverter();
        }
        public HoaDonDTO EntityToDTO(HoaDon hoaDon)
        {
            return new HoaDonDTO
            {
                KhachHangID = hoaDon.KhachHangID,
                GhiChu = hoaDon.GhiChu,
                MaGiaoDich = hoaDon.MaGiaoDich,
                TenHoaDon = hoaDon.TenHoaDon,
                ThoiGianCapNhap = hoaDon.ThoiGianCapNhap,
                ThoiGianTao = hoaDon.ThoiGianTao,
                TongTien = hoaDon.TongTien,
                chiTietHoaDonDTOs = context.ChiTietHoaDon.Where(x => x.HoaDonID == hoaDon.HoaDonID).Select(x => chiTietConverter.EntityToDTO(x)).AsQueryable(),
            };
        }
    }
}

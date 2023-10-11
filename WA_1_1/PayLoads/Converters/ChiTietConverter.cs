using WA_1_1.Entitites;
using WA_1_1.PayLoads.DTOs;

namespace WA_1_1.PayLoads.Converters
{
    public class ChiTietConverter
    {
        public ChiTietHoaDonDTO EntityToDTO(ChiTietHoaDon ct)
        {
            return new ChiTietHoaDonDTO
            {
                SanPhamID = ct.SanPhamID,
                DonViTinh = ct.DonViTinh,
                SoLuong = ct.SoLuong,
                ThanhTien = ct.ThanhTien
            };
        }
    }
}

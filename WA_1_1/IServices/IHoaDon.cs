using WA_1_1.Entitites;
using WA_1_1.PayLoads.DTOs;
using WA_1_1.PayLoads.Requests;
using WA_1_1.PayLoads.Responses;
using WA_1_1.Result;

namespace WA_1_1.IServices
{
    public interface IHoaDon
    {
        ErrorType ThemHoaDon(HoaDon newHoaDon);
        ErrorType XoaHoaDon(int id);
        ErrorType UpdateHoaDon(HoaDon HoaDonCanSua);
        ResponseObject<HoaDonDTO> ThemHoaDonRequest(ThemHoaDonRequest request);
        IQueryable<HoaDonDTO> GetAll();
        IQueryable<HoaDonDTO> GetHoaDonTheoNgay();
        ResponseObject<HoaDonDTO> XoaHoaDon(XoaHoaDonRequest xoaHoaDonRequest);
        ResponseObject<HoaDonDTO> SuaHoaDon(SuaHoaDonRequest suaHoaDonRequest);
        IQueryable<HoaDonDTO> GetDuLieuTheoBoLoc(string? keySearch=null,int? year = null, int? month = null, DateTime? tuNgay = null, DateTime? denNgay = null, int? GiaTu = null, int? GiaDen = null);
    }
}

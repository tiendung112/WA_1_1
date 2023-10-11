using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WA_1_1.Context;
using WA_1_1.Entitites;
using WA_1_1.IServices;
using WA_1_1.Pagination;
using WA_1_1.PayLoads.Converters;
using WA_1_1.PayLoads.DTOs;
using WA_1_1.PayLoads.Requests;
using WA_1_1.PayLoads.Responses;
using WA_1_1.Result;

namespace WA_1_1.Services
{
    public class HoaDonServices : IHoaDon
    {
        private readonly HoaDonContext context;

        private readonly ResponseObject<HoaDonDTO> responseObject;
        private readonly HoaDonConverter converter;

        public HoaDonServices()
        {
            context = new HoaDonContext();
            responseObject = new ResponseObject<HoaDonDTO>();
            converter = new HoaDonConverter();
        }

        public ErrorType ThemHoaDon(HoaDon newHoaDon)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                try
                {
                    if (context.HoaDon.Any(x => x.HoaDonID == newHoaDon.HoaDonID))
                    {
                        return ErrorType.HoaDonDaTonTai;
                    }
                    else
                    {
                        //tim ra so luong hoa don de tao ma giao dich
                        //var lstHoaDon = context.HoaDon.Where(x=>x.ThoiGianTao==newHoaDon.ThoiGianTao).ToList();
                        var lstHoaDon = context.HoaDon.Where(x => x.ThoiGianTao.Day == newHoaDon.ThoiGianTao.Day
                            && x.ThoiGianTao.Month == newHoaDon.ThoiGianTao.Month
                            && x.ThoiGianTao.Year == newHoaDon.ThoiGianTao.Year).ToList();

                        newHoaDon.MaGiaoDich = $"{newHoaDon.ThoiGianTao.Year}" +
                            $"{newHoaDon.ThoiGianTao.Month}" +
                            $"{newHoaDon.ThoiGianTao.Day}" +
                            $"_{String.Format("{0:000}", lstHoaDon.Count() + 1)}";
                        var lstCT = newHoaDon.chiTietHoaDon;
                        newHoaDon.chiTietHoaDon = null;
                        context.Add(newHoaDon);
                        context.SaveChanges();
                        //thêm chi tiết hoá đơn 
                        if (lstCT.Count() > 0)
                        {
                            foreach (var item in lstCT)
                            {
                                //kiểm tra tồn tại sản phẩm không 
                                if (context.SanPham.Any(x => x.SanPhamID == item.SanPhamID))
                                {
                                    var spcantim = context.SanPham.FirstOrDefault(x => x.SanPhamID == item.SanPhamID);
                                    item.HoaDonID = newHoaDon.HoaDonID;
                                    item.ThanhTien = item.SoLuong * spcantim.GiaThanh;
                                    newHoaDon.TongTien += item.ThanhTien;
                                    context.Update(newHoaDon);

                                    context.Add(item);
                                    context.SaveChanges();
                                }
                                else
                                    return ErrorType.SanPhamChuaTonTai;
                            }
                        }

                        trans.Commit();
                        return ErrorType.ThanhCong;
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return ErrorType.ThatBai;
                }
            }
        }

        public ResponseObject<HoaDonDTO> ThemHoaDonRequest(ThemHoaDonRequest request)
        {
            var khachHang = context.KhachHang.FirstOrDefault(x => x.KhachHangID == request.KhachHangID);
            if (khachHang == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Khách hàng không tồn tại", null);
            }
            var hoaDon = new HoaDon();
            hoaDon.KhachHangID = request.KhachHangID;
            hoaDon.GhiChu = request.GhiChu;
            hoaDon.TenHoaDon = request.TenHoaDon;
            hoaDon.ThoiGianTao = DateTime.Now;
            hoaDon.ThoiGianCapNhap = null;
            hoaDon.TongTien = 0;
            hoaDon.MaGiaoDich = TaoMaGiaoDich(hoaDon.ThoiGianTao, context.HoaDon.Where(x=>x.ThoiGianTao==hoaDon.ThoiGianTao).ToList());
            context.HoaDon.Add(hoaDon);
            context.SaveChanges();
            List<ChiTietHoaDon> list = ThemChiTietHoaDon(hoaDon.HoaDonID, request.themChiTietHoaDonRequests);
            hoaDon.chiTietHoaDon = list;
            context.Update(hoaDon);
            context.SaveChanges();

            HoaDonDTO hoaDonDTO = converter.EntityToDTO(hoaDon);
            return responseObject.ResponseSuccess("Thêm hoá đơn thành công", hoaDonDTO);

        }
        private string TaoMaGiaoDich(DateTime nt , List<HoaDon>ct)
        {
            //newHoaDon.MaGiaoDich = $"{newHoaDon.ThoiGianTao.Year}" +
            //                $"{newHoaDon.ThoiGianTao.Month}" +
            //                $"{newHoaDon.ThoiGianTao.Day}" +
            //                $"_{String.Format("{0:000}", lstHoaDon.Count() + 1)}";
            string maGiaoDich = nt.ToString("yyyyMMdd")+$"{ct.Count()+1}";
            return maGiaoDich;
        }
        private List<ChiTietHoaDon> ThemChiTietHoaDon(int hoaDonId, List<ThemChiTietHoaDonRequest> requests)
        {
            var hoaDon = context.HoaDon.FirstOrDefault(x => x.HoaDonID == hoaDonId);
            if (hoaDon == null)
            {
                return null;
            }
            List<ChiTietHoaDon> list = new List<ChiTietHoaDon>();
            foreach (var request in requests)
            {
                ChiTietHoaDon ct = new ChiTietHoaDon();
                ct.HoaDonID = hoaDonId;
                var sanPham = context.SanPham.FirstOrDefault(x => x.SanPhamID == request.SanPhamID);
                if (sanPham == null)
                {
                    throw new Exception("Sản phẩm không tồn tại");
                }
                ct.SanPhamID = request.SanPhamID;
                ct.SoLuong = request.SoLuong;
                ct.DonViTinh = request.DonViTinh;
                ct.ThanhTien = request.SoLuong * sanPham.GiaThanh;
                list.Add(ct);
            }
            context.ChiTietHoaDon.AddRange(list);
            context.SaveChanges();
            double? tongTien = 0;
            //hoaDon.chiTietHoaDon.ToList().ForEach(x =>
            //{
            //    tongTien += x.ThanhTien;
            //});
            hoaDon.TongTien = hoaDon.chiTietHoaDon.Sum(x => x.ThanhTien);
            //hoaDon.TongTien = tongTien;
            context.Update(hoaDon);
            context.SaveChanges();
            return list;

        }

        public ErrorType UpdateHoaDon(HoaDon HoaDonCanSua)
        {
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    //tìm  hoá đơn cần sửa 
                    var hoadon = context.HoaDon.FirstOrDefault(x => x.HoaDonID == HoaDonCanSua.HoaDonID);

                    if (hoadon != null && context.KhachHang.Any(x => x.KhachHangID == HoaDonCanSua.KhachHangID))
                    {
                        var lstCTHD = context.ChiTietHoaDon.Where(x => x.HoaDonID == HoaDonCanSua.HoaDonID).ToList();
                        foreach (var item in lstCTHD)
                        {

                            if (context.SanPham.Any(x => x.SanPhamID == item.SanPhamID))
                            {
                                var spcantim = context.SanPham.FirstOrDefault(x => x.SanPhamID == item.SanPhamID);
                                var ctUpdate = context.ChiTietHoaDon.FirstOrDefault(x => x.ChiTietHoaDonID == item.ChiTietHoaDonID);

                                //chỉnh sửa chi tiết hoá đơn 
                                //ctUpdate.HoaDonID = item.HoaDonID;
                                //ctUpdate.SanPhamID = item.HoaDonID;
                                ctUpdate.SanPhamID = item.SanPhamID;
                                ctUpdate.SoLuong = item.SoLuong;
                                ctUpdate.ThanhTien = ctUpdate.SoLuong * spcantim?.GiaThanh;
                                context.Update(ctUpdate);
                                context.SaveChanges();
                            }
                            else Console.WriteLine("Không tồn tại sản phẩm ");
                        }
                        //update lại tổng tiền và hoá đơn 
                        var lstCTHDmoi = context.ChiTietHoaDon.Where(x => x.HoaDonID == HoaDonCanSua.HoaDonID).ToList();
                        HoaDonCanSua.TongTien = 0;
                        foreach (var item in lstCTHDmoi)
                        {
                            HoaDonCanSua.TongTien += item.ThanhTien;
                        }
                        hoadon.TongTien = HoaDonCanSua.TongTien;
                        hoadon.KhachHangID = HoaDonCanSua.KhachHangID;
                        hoadon.TenHoaDon = HoaDonCanSua.TenHoaDon;
                        hoadon.ThoiGianCapNhap = DateTime.Now;
                        hoadon.GhiChu = HoaDonCanSua.GhiChu;

                        context.Update(hoadon);
                        context.SaveChanges();
                        Console.WriteLine("thanh cong");
                        tran.Commit();
                        return ErrorType.ThanhCong;
                    }

                    else
                        //Console.WriteLine("that bai");
                        return ErrorType.HoaDonKhongTonTai;

                }
                catch (Exception)
                {
                    tran.Rollback();
                    Console.WriteLine("that bai");
                    return ErrorType.ThatBai;
                }


            }
        }
        public ErrorType XoaHoaDon(int id)
        {
            var hoadoncanxoa = context.HoaDon.FirstOrDefault(x => x.HoaDonID == id);
            //tìm kiếm hoá đơn cần xoá
            if (hoadoncanxoa != null)
            {
                var lstCthoadon = context.ChiTietHoaDon.Where(x => x.HoaDonID == id);
                foreach (var item in lstCthoadon)
                {
                    context.ChiTietHoaDon.Remove(item);
                }
                context.HoaDon.Remove(hoadoncanxoa);
                Console.WriteLine("thành công");
                context.SaveChanges();
                return ErrorType.ThanhCong;
            }
            else
            {
                Console.WriteLine("thất bại");
                return ErrorType.HoaDonKhongTonTai;
            }
        }

        public IQueryable<HoaDonDTO> GetAll()
        {
            return context.HoaDon.Include(x => x.chiTietHoaDon).Select(x => converter.EntityToDTO(x));
        }

        public ResponseObject<HoaDonDTO> XoaHoaDon(XoaHoaDonRequest xoaHoaDonRequest)
        {
            var hoadoncanxoa = context.HoaDon.FirstOrDefault(x => x.HoaDonID == xoaHoaDonRequest.HoaDonID);

            if (hoadoncanxoa != null)
            {
                var lstCthoadon = context.ChiTietHoaDon.Where(x => x.HoaDonID == xoaHoaDonRequest.HoaDonID);
                foreach (var item in lstCthoadon)
                {
                    context.ChiTietHoaDon.Remove(item);
                }
                context.HoaDon.Remove(hoadoncanxoa);
                Console.WriteLine("thành công");
                context.SaveChanges();
                return responseObject.ResponseSuccess("Thành công ", null);
            }
            else
            {
                Console.WriteLine("thất bại");
                return responseObject.ResponseError(403, "Không tồn tại", null);
            }

        }

        public ResponseObject<HoaDonDTO> SuaHoaDon(SuaHoaDonRequest request)
        {
            var hoadoncantim = context.HoaDon.FirstOrDefault(x => x.HoaDonID == request.HoaDonID);
            List<ChiTietHoaDon> lstCT = context.ChiTietHoaDon.Where(x => x.HoaDonID == request.HoaDonID).ToList();
            lstCT = SuaChiTietHoaDon(hoadoncantim.HoaDonID, request.suaChiTietHoaDonRequests);

            //hoadoncantim.chiTietHoaDon = lstCT;
            context.ChiTietHoaDon.UpdateRange(lstCT);
            context.SaveChanges();

            var khachHang = context.KhachHang.FirstOrDefault(x => x.KhachHangID == request.KhachHangID);

            if (khachHang == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "khách hàng không tồn tại ", null);

            }
            hoadoncantim.KhachHangID = request.KhachHangID;
            hoadoncantim.GhiChu = request.GhiChu;
            hoadoncantim.TenHoaDon = request.TenHoaDon;
            hoadoncantim.ThoiGianCapNhap = DateTime.Now;
            hoadoncantim.TongTien = lstCT.Sum(x => x.ThanhTien);

            /*foreach (var ct in lstCT)
            {
                hoadoncantim.TongTien += ct.ThanhTien;
            }*/
            context.HoaDon.Update(hoadoncantim);
            context.SaveChanges();

            //chuyển đổi sang dto 
            HoaDonDTO hoaDonDTO = converter.EntityToDTO(hoadoncantim);
            return responseObject.ResponseSuccess("Sửa hoá đơn hoá đơn thành công", hoaDonDTO);
        }
        private List<ChiTietHoaDon> SuaChiTietHoaDon(int hoaDonID, List<SuaChiTietHoaDonRequest> request)
        {
            var HoaDon = context.HoaDon.FirstOrDefault(x => x.HoaDonID == hoaDonID);
            if (HoaDon == null)
            {
                return null;
            }
            var lstChiTiet = context.ChiTietHoaDon.Where(x => x.HoaDonID == hoaDonID).ToList();
            foreach (var ct in lstChiTiet)
            {
                foreach (var ch in request)
                {
                    var sp = context.SanPham.FirstOrDefault(x => x.SanPhamID == ct.SanPhamID);

                    //kiểm tra tồn tại sản phẩm
                    if (sp == null)
                    {
                        // Trả về lỗi nếu sản phẩm không tồn tại
                        return null;
                    }
                    ct.HoaDonID = hoaDonID;
                    ct.SanPhamID = ch.SanPhamID;
                    ct.SoLuong = ch.SoLuong;
                    ct.DonViTinh = ch.DonViTinh;

                    ct.ThanhTien = ch.SoLuong * sp.GiaThanh;
                }
            }
            return lstChiTiet;
        }
        //Viết API lấy dữ liệu hóa đơn cùng chi tiết của hóa đơn đó, sắp xếp theo thời gian tạo mới nhất.
        public IQueryable<HoaDonDTO> GetHoaDonTheoNgay()
        {
            return context.HoaDon
                  .Include(x => x.chiTietHoaDon)
                  .Select(x => converter.EntityToDTO(x))
                  .ToList()
                  .OrderByDescending(x => x.ThoiGianTao).AsQueryable();
        }

        public IQueryable<HoaDonDTO> GetDuLieuTheoBoLoc(string? keySearch = null,int ? year = null, int? month = null, DateTime? tuNgay = null, DateTime? denNgay = null, int? GiaTu = null, int? GiaDen=null)
        {
            var lstHoaDon = context.HoaDon
                 .Include(x => x.chiTietHoaDon)
                 .Select(x => converter.EntityToDTO(x))
                 ;/*.AsEnumerable()  // Chuyển sang đánh giá phía client
                 .Where(x => x.ThoiGianTao.Year == year)
                 .AsQueryable();*/
            //tháng năm
            if (year.HasValue&& month.HasValue)
            {
                lstHoaDon=lstHoaDon.ToList().Where(x=>x.ThoiGianTao.Year==year)
                    .Where(x=>x.ThoiGianTao.Month==month).AsQueryable();
            }
            if (tuNgay.HasValue)
            {
                lstHoaDon = lstHoaDon.ToList()
                    .Where(x => x.ThoiGianTao.Date >= tuNgay).AsQueryable();
            }
            if(denNgay.HasValue)
            {
                lstHoaDon = lstHoaDon.ToList()
                    .Where(x => x.ThoiGianTao.Date <= denNgay).AsQueryable();
            }
            if (GiaTu.HasValue)
            {
                lstHoaDon=lstHoaDon.ToList()
                    .Where(x=>x.TongTien>=GiaTu).AsQueryable();
            }
            if(GiaDen.HasValue)
            {
                lstHoaDon = lstHoaDon.ToList()
                    .Where(x => x.TongTien <= GiaDen).AsQueryable();

            }
            if (!string.IsNullOrEmpty(keySearch))
            {
                lstHoaDon =lstHoaDon.ToList().Where(x=>x.TenHoaDon.ToLower()==keySearch.ToLower()
                || x.MaGiaoDich.ToLower()==keySearch.ToLower()).AsQueryable();
            }
            return lstHoaDon;
        }

        public IQueryable<HoaDonDTO> GetAll(Pagintation pagintation)
        {
            var lstHD = context.HoaDon.Include(x => x.chiTietHoaDon).Select(x => converter.EntityToDTO(x));
            return lstHD;
        }
    }
}

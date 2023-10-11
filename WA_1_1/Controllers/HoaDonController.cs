using Microsoft.AspNetCore.Mvc;
using WA_1_1.Entitites;
using WA_1_1.Pagination;
using WA_1_1.PayLoads.DTOs;
using WA_1_1.PayLoads.Requests;
using WA_1_1.Services;

namespace WA_1_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private static HoaDonServices services = new HoaDonServices();

        [HttpPost("themPhieuThu")]
        public IActionResult ThemPhieuThu([FromBody] HoaDon newHoaDon)
        {
            var ret = services.ThemHoaDon(newHoaDon);
            if (ret == Result.ErrorType.ThanhCong)
            {
                return Ok("thành công");
            }
            else
            {
                return BadRequest("thất bại");
            }

        }
        [HttpPost("ThemHoaDonRequest")]
        public IActionResult ThemHoaDonRequest(ThemHoaDonRequest request)
        {
            return Ok(services.ThemHoaDonRequest(request));
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(services.GetAll());
        }
        [HttpGet("getAllPhaNTrang")]
        public IActionResult GetAllPT([FromQuery] Pagintation pagintation)
        {
            var hd = services.GetAll(pagintation);
            var hdRs = PageResult<HoaDonDTO>.toPageResult(pagintation, hd);
            pagintation.TotalCount = hd.Count();
            var res =new PageResult<HoaDonDTO> (pagintation,hdRs);
            return Ok(res);
        }
        [HttpDelete("XoaHoaDon")]
        public IActionResult Delete(XoaHoaDonRequest id)
        {
            return Ok(services.XoaHoaDon(id));
        }
        [HttpPut("SuaHoaDon")]
        public IActionResult update(SuaHoaDonRequest suaHoaDonRequest)
        {
            return Ok(services.SuaHoaDon(suaHoaDonRequest));
        }
        [HttpGet("sapXepHoaDonTheoNgayTao")]
        public IActionResult getHoaDonNgayTao()
        {
            return Ok(services.GetHoaDonTheoNgay());
        }
        [HttpGet("layHoaDonTheoNam")]
        public IActionResult getTheoNam([FromQuery] string? keySearch = null, int? year = null, int? month = null, DateTime? tuNgay = null, DateTime? denNgay = null, int? GiaTu = null, int? GiaDen = null)
        {
            return Ok(services.GetDuLieuTheoBoLoc(keySearch,year,month,tuNgay,denNgay,GiaTu,GiaDen));

        }

    }
}

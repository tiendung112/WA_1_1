using WA_1_1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
    .AddNewtonsoftJson
    (x => x.SerializerSettings
        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


HoaDonServices hoaDonServices = new HoaDonServices();


//HoaDon hoaDon = new HoaDon()
//{
//    HoaDonID = 23,
//    KhachHangID = 1,
//    TenHoaDon = "Bán Sach",
//    ThoiGianCapNhap = null,
//    GhiChu = "aaaa435434aa",
//    TongTien = 0,
//    chiTietHoaDon = new List<ChiTietHoaDon>()
//    {
//        new ChiTietHoaDon()
//        {
//            SanPhamID=2,
//            SoLuong=2,
//            DonViTinh="cái",
//            ThanhTien=0,
//        },

//    }

//};
//hoaDonServices.UpdateHoaDon(hoaDon);
//Console.WriteLine();
//HoaDon hoaDon = new HoaDon()
//{
//    MaGiaoDich="",
//    KhachHangID = 1,
//    TenHoaDon = "Bán Sach",
//    ThoiGianCapNhap = null,
//    GhiChu = "aaaa435434aa",
//    TongTien = 0,
//    ThoiGianTao = new DateTime(2022,12,12),
//    chiTietHoaDon = new List<ChiTietHoaDon>()
//    {
//        new ChiTietHoaDon()
//        {
//            SanPhamID=4,
//            SoLuong=12,
//            DonViTinh="cái",
//            ThanhTien=0,
//        },
//        new ChiTietHoaDon()
//        {
//            SanPhamID=4,
//            SoLuong =12,
//            DonViTinh="cái",
//            ThanhTien =0
//        }
//    }

//};
//hoaDonServices.ThemHoaDon(hoaDon);
//Console.WriteLine();
//hoaDonServices.ThemHoaDon(hoaDon);



//
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

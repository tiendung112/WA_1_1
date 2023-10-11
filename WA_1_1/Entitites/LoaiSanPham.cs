namespace WA_1_1.Entitites
{
    public class LoaiSanPham
    {
        public int LoaiSanPhamID { get; set; }
        public string TenLoaiSanPham { get; set; }

        public IList<SanPham> SanPham { get; set; }

    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WA_1_1.Context;

#nullable disable

namespace WA_1_1.Migrations
{
    [DbContext(typeof(HoaDonContext))]
    [Migration("20231005081354_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WA_1_1.Entitites.ChiTietHoaDon", b =>
                {
                    b.Property<int>("ChiTietHoaDonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChiTietHoaDonID"));

                    b.Property<string>("DonViTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoaDonID")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<double?>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("ChiTietHoaDonID");

                    b.HasIndex("HoaDonID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("ChiTietHoaDon");
                });

            modelBuilder.Entity("WA_1_1.Entitites.HoaDon", b =>
                {
                    b.Property<int>("HoaDonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HoaDonID"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KhachHangID")
                        .HasColumnType("int");

                    b.Property<string>("MaGiaoDich")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHoaDon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ThoiGianCapNhap")
                        .HasColumnType("date");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("date");

                    b.Property<int?>("TongTien")
                        .HasColumnType("int");

                    b.HasKey("HoaDonID");

                    b.HasIndex("KhachHangID");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("WA_1_1.Entitites.KhachHang", b =>
                {
                    b.Property<int>("KhachHangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KhachHangID"));

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KhachHangID");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("WA_1_1.Entitites.LoaiSanPham", b =>
                {
                    b.Property<int>("LoaiSanPhamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoaiSanPhamID"));

                    b.Property<string>("TenLoaiSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoaiSanPhamID");

                    b.ToTable("LoaiSanPham");
                });

            modelBuilder.Entity("WA_1_1.Entitites.SanPham", b =>
                {
                    b.Property<int>("SanPhamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SanPhamID"));

                    b.Property<double>("GiaThanh")
                        .HasColumnType("float");

                    b.Property<string>("KiHieuSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoaiSanPhamID")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SanPhamID");

                    b.HasIndex("LoaiSanPhamID");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("WA_1_1.Entitites.ChiTietHoaDon", b =>
                {
                    b.HasOne("WA_1_1.Entitites.HoaDon", "HoaDon")
                        .WithMany("chiTietHoaDon")
                        .HasForeignKey("HoaDonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WA_1_1.Entitites.SanPham", "SanPham")
                        .WithMany("ChiTietHoaDon")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WA_1_1.Entitites.HoaDon", b =>
                {
                    b.HasOne("WA_1_1.Entitites.KhachHang", "KhachHang")
                        .WithMany("hoaDon")
                        .HasForeignKey("KhachHangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("WA_1_1.Entitites.SanPham", b =>
                {
                    b.HasOne("WA_1_1.Entitites.LoaiSanPham", "LoaiSanPham")
                        .WithMany("SanPham")
                        .HasForeignKey("LoaiSanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiSanPham");
                });

            modelBuilder.Entity("WA_1_1.Entitites.HoaDon", b =>
                {
                    b.Navigation("chiTietHoaDon");
                });

            modelBuilder.Entity("WA_1_1.Entitites.KhachHang", b =>
                {
                    b.Navigation("hoaDon");
                });

            modelBuilder.Entity("WA_1_1.Entitites.LoaiSanPham", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WA_1_1.Entitites.SanPham", b =>
                {
                    b.Navigation("ChiTietHoaDon");
                });
#pragma warning restore 612, 618
        }
    }
}

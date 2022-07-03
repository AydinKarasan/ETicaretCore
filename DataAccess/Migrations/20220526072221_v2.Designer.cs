﻿// <auto-generated />
using System;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ETicaretContext))]
    [Migration("20220526072221_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entities.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Aciklamasi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("DataAccess.Entities.Magaza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Puani")
                        .HasColumnType("tinyint");

                    b.Property<bool>("SanalMi")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Magazalar");
                });

            modelBuilder.Entity("DataAccess.Entities.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Aciklama")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("BirimFiyati")
                        .HasColumnType("float");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SonKullanmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("StokMiktari")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("Urunler");
                });

            modelBuilder.Entity("DataAccess.Entities.UrunMagaza", b =>
                {
                    b.Property<int>("UrunId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("MagazaId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("UrunId", "MagazaId");

                    b.HasIndex("MagazaId");

                    b.ToTable("UrunMagazalar");
                });

            modelBuilder.Entity("DataAccess.Entities.Urun", b =>
                {
                    b.HasOne("DataAccess.Entities.Kategori", "Kategori")
                        .WithMany("Urunler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("DataAccess.Entities.UrunMagaza", b =>
                {
                    b.HasOne("DataAccess.Entities.Magaza", "Magaza")
                        .WithMany("UrunMagazalar")
                        .HasForeignKey("MagazaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Urun", "Urun")
                        .WithMany("UrunMagazalar")
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Magaza");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("DataAccess.Entities.Kategori", b =>
                {
                    b.Navigation("Urunler");
                });

            modelBuilder.Entity("DataAccess.Entities.Magaza", b =>
                {
                    b.Navigation("UrunMagazalar");
                });

            modelBuilder.Entity("DataAccess.Entities.Urun", b =>
                {
                    b.Navigation("UrunMagazalar");
                });
#pragma warning restore 612, 618
        }
    }
}

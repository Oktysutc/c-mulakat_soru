﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using c_mulakat_soru.Utility;

#nullable disable

namespace c_mulakat_soru.Migrations
{
    [DbContext(typeof(UygulamaDbContext))]
    [Migration("20240226124047_SatinalmalarTablosuEkle")]
    partial class SatinalmalarTablosuEkle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("c_mulakat_soru.Models.Kurs", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<double>("Fiyat")
                        .HasColumnType("float");

                    b.Property<string>("Konu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KursAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KursTuruId")
                        .HasColumnType("int");

                    b.Property<string>("ResimUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Yayinlayan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KursTuruId");

                    b.ToTable("Kurslar");
                });

            modelBuilder.Entity("c_mulakat_soru.Models.KursTuru", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("KursTurleri");
                });

            modelBuilder.Entity("c_mulakat_soru.Models.Satinal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KursId")
                        .HasColumnType("int");

                    b.Property<int?>("KursTuruId")
                        .HasColumnType("int");

                    b.Property<int>("OgrenciId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KursTuruId");

                    b.ToTable("Satinalmalar");
                });

            modelBuilder.Entity("c_mulakat_soru.Models.Kurs", b =>
                {
                    b.HasOne("c_mulakat_soru.Models.KursTuru", "kursTuru")
                        .WithMany()
                        .HasForeignKey("KursTuruId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kursTuru");
                });

            modelBuilder.Entity("c_mulakat_soru.Models.Satinal", b =>
                {
                    b.HasOne("c_mulakat_soru.Models.Kurs", "Kurs")
                        .WithMany()
                        .HasForeignKey("KursTuruId");

                    b.Navigation("Kurs");
                });
#pragma warning restore 612, 618
        }
    }
}

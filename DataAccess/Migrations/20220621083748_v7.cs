using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imaj",
                table: "Urunler",
                type: "image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImajUzantisi",
                table: "Urunler",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imaj",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "ImajUzantisi",
                table: "Urunler");
        }
    }
}

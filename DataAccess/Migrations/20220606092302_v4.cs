using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Ulkeler");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Sehirler");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Roller");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Magazalar");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Kategoriler");

            migrationBuilder.AddColumn<int>(
                name: "Cinsiyet",
                table: "KullaniciDetaylari",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cinsiyet",
                table: "KullaniciDetaylari");

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Urunler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Ulkeler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Sehirler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Roller",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Magazalar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Kategoriler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

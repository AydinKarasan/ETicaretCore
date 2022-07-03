using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ulkeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulkeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Roller_RolId",
                        column: x => x.RolId,
                        principalTable: "Roller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sehirler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UlkeId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sehirler_Ulkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KullaniciDetaylari",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UlkeId = table.Column<int>(type: "int", nullable: false),
                    SehirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciDetaylari", x => x.KullaniciId);
                    table.ForeignKey(
                        name: "FK_KullaniciDetaylari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KullaniciDetaylari_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KullaniciDetaylari_Ulkeler_UlkeId",
                        column: x => x.UlkeId,
                        principalTable: "Ulkeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciDetaylari_SehirId",
                table: "KullaniciDetaylari",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciDetaylari_UlkeId",
                table: "KullaniciDetaylari",
                column: "UlkeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehirler_UlkeId",
                table: "Sehirler",
                column: "UlkeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciDetaylari");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Sehirler");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "Ulkeler");
        }
    }
}

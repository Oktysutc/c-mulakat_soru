using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace c_mulakat_soru.Migrations
{
    /// <inheritdoc />
    public partial class SatinalmalarTablosuEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResimUrl",
                table: "Kurslar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Satinalmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    KursTuruId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satinalmalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satinalmalar_Kurslar_KursTuruId",
                        column: x => x.KursTuruId,
                        principalTable: "Kurslar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Satinalmalar_KursTuruId",
                table: "Satinalmalar",
                column: "KursTuruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Satinalmalar");

            migrationBuilder.AlterColumn<string>(
                name: "ResimUrl",
                table: "Kurslar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

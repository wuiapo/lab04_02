using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab04_01.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NamThanhLap",
                table: "Khoas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TongSoGiangVien",
                table: "Khoas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamThanhLap",
                table: "Khoas");

            migrationBuilder.DropColumn(
                name: "TongSoGiangVien",
                table: "Khoas");
        }
    }
}

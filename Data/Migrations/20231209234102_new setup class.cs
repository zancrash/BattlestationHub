using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattlestationHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class newsetupclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StationName",
                table: "Battlestation",
                newName: "SetupName");

            migrationBuilder.RenameColumn(
                name: "StationDescription",
                table: "Battlestation",
                newName: "SetupDesc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SetupName",
                table: "Battlestation",
                newName: "StationName");

            migrationBuilder.RenameColumn(
                name: "SetupDesc",
                table: "Battlestation",
                newName: "StationDescription");
        }
    }
}

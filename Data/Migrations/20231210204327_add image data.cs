using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattlestationHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class addimagedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SetupImg",
                table: "Battlestation",
                newName: "ImgData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgData",
                table: "Battlestation",
                newName: "SetupImg");
        }
    }
}

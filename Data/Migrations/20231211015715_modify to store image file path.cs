using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattlestationHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifytostoreimagefilepath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgData",
                table: "Battlestation",
                newName: "ImgPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgPath",
                table: "Battlestation",
                newName: "ImgData");
        }
    }
}

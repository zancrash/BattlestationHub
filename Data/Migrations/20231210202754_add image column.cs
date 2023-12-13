using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattlestationHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class addimagecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SetupImg",
                table: "Battlestation",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetupImg",
                table: "Battlestation");
        }
    }
}

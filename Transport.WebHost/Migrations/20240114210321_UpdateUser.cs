using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.WebHost.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LastPingedLatitude",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LastPingedLongitude",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPingedLatitude",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastPingedLongitude",
                table: "Users");
        }
    }
}

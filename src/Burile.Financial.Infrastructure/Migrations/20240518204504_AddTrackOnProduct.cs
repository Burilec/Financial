using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burile.Financial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackOnProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Track",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Track",
                table: "Products");
        }
    }
}

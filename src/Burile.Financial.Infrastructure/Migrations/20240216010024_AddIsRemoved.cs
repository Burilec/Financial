using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burile.Financial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Portfolios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Portfolios");
        }
    }
}

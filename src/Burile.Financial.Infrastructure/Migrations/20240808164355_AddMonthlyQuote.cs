using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burile.Financial.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlyQuote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Portfolios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "MonthlyQuotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ApiId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OpeningPrice = table.Column<decimal>(type: "decimal(60,30)", precision: 60, scale: 30, nullable: false),
                    ClosingPrice = table.Column<decimal>(type: "decimal(60,30)", precision: 60, scale: 30, nullable: false),
                    HighestPrice = table.Column<decimal>(type: "decimal(60,30)", precision: 60, scale: 30, nullable: false),
                    LowestPrice = table.Column<decimal>(type: "decimal(60,30)", precision: 60, scale: 30, nullable: false),
                    AdjustedClose = table.Column<decimal>(type: "decimal(60,30)", precision: 60, scale: 30, nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false),
                    DividendAmount = table.Column<decimal>(type: "decimal(60,30)", precision: 60, scale: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyQuotes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyQuotes_ApiId",
                table: "MonthlyQuotes",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyQuotes_DateTime",
                table: "MonthlyQuotes",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyQuotes_Id",
                table: "MonthlyQuotes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyQuotes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Portfolios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}

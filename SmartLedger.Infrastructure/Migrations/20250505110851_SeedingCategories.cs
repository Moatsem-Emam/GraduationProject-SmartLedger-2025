using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow;

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryName", "CreatedAt" },
                values: new object[,]
                {
    { "تسويات", now },
    { "صناديق", now },
    { "العليات", now },
    { "تسويات صناديق", now },
    { "تسويات موارد", now },
    { "موارد ذاتية", now },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryName",
                keyValues: new object[]
                {
    "تسويات",
    "صناديق",
    "العليات",
    "تسويات صناديق",
    "تسويات موارد",
    "موارد ذاتية",
                });
        }
    }
}

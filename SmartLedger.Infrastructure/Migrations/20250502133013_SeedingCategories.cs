using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow;

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryName", "CreatedAt" },
                values: new object[,]
                {
            { "Cash", now },
            { "Categories Receivable", now },
            { "Inventory", now },
            { "Prepaid Expenses", now },
            { "Categories Payable", now },
            { "Revenue", now },
            { "Expenses", now },
            { "Retained Earnings", now }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryName",
                keyValues: new object[]
                {
            "Cash",
            "Categories Receivable",
            "Inventory",
            "Prepaid Expenses",
            "Categories Payable",
            "Revenue",
            "Expenses",
            "Retained Earnings"
                });
        }

    }
}

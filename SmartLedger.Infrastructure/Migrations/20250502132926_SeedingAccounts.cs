using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow;

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountName", "CreatedAt" },
                values: new object[,]
                {
            { "Cash", now },
            { "Accounts Receivable", now },
            { "Inventory", now },
            { "Prepaid Expenses", now },
            { "Accounts Payable", now },
            { "Revenue", now },
            { "Expenses", now },
            { "Retained Earnings", now }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountName",
                keyValues: new object[]
                {
            "Cash",
            "Accounts Receivable",
            "Inventory",
            "Prepaid Expenses",
            "Accounts Payable",
            "Revenue",
            "Expenses",
            "Retained Earnings"
                });
        }

    }
}

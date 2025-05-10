using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seeding Journal Entries
            for (int i = 45; i <= 76; i++)
            {
                migrationBuilder.InsertData(
                    table: "JournalEntries",
                    columns: new[] { "Name", "Description", "CategoryId", "UserId", "CreatedAt" },
                    values: new object[]
                    {
                $"قيد رقم {i}",
                $"الوصف الخاص بالقيد رقم {i}",
                (i % 6) + 1, // CategoryId: 1-6
                1, // UserId
                DateTime.UtcNow
                    }
                );
            }

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (long i = 45; i <= 76; i++)
            {
                migrationBuilder.DeleteData(
                    table: "JournalEntries",
                    keyColumn: "Id",
                    keyValue: i
                );
            }

            
        }

    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seeding Journal Entry Details
            var rand = new Random();
            for (int i = 141; i <= 172; i++)
            {
                int detailCount = rand.Next(3, 6); // 3 to 5 details per entry
                var usedAccounts = new HashSet<int>();
                for (int j = 0; j < detailCount; j++)
                {
                    int accId;
                    do { accId = rand.Next(1, 9); } while (!usedAccounts.Add(accId));

                    long debit = 0, credit = 0;
                    if (j == 0)
                        debit = 2000;
                    else if (j == 1)
                        credit = 2000;
                    else
                        debit = rand.Next(100, 1000);

                    migrationBuilder.InsertData(
                        table: "JournalEntryDetails",
                        columns: new[] { "JournalEntryId", "AccountId", "DebitAmount", "CreditAmount", "CreatedAt" },
                        values: new object[]
                        {
                    i,
                    accId,
                    debit,
                    credit,
                    DateTime.UtcNow
                        }
                    );
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (long i = 1; i <= 150; i++) // max 5 * 30
            {
                migrationBuilder.DeleteData(
                    table: "JournalEntryDetails",
                    keyColumn: "Id",
                    keyValue: i
                );
            }
        }
    }
}

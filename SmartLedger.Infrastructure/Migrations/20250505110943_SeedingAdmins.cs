using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAdmins : Migration
    {
        /// <inheritdoc />
        // Plain Text password => 159$@MoatsemHussain@$159
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users", // make sure your table is called "Users"
                columns: new[] { "Email", "PasswordHash", "Role" , "CreatedAt" },
                values: new object[,]
                {
                    { 
                   "motsememamhussain@gmail.com",
                   "$2a$12$CNhUncEHjuhcucSsMP3s4OAFvxdOtJKubu1NT7Np5oWhsZCJ/cHE.",
                   "Admin",
                    DateTime.Now}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Users", keyColumn: "Id", keyValue: 1);

        }
    }
}

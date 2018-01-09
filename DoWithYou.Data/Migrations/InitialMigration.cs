using System;
using System.Data;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore.Migrations;
using Serilog;

namespace DoWithYou.Data.Migrations
{
    public class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Migrating Up on {Class}", nameof(InitialMigration));

            migrationBuilder.CreateTable(
                nameof(User),
                table => new
                {
                    ID = table.Column<long>(SqlDbType.Int.ToString()),
                    CreationDate = table.Column<DateTime>(SqlDbType.DateTime2.ToString()),
                    ModifiedDate = table.Column<DateTime>(SqlDbType.DateTime2.ToString()),
                    Username = table.Column<string>($"{SqlDbType.VarChar.ToString()}(max)"),
                    Password = table.Column<string>($"{SqlDbType.VarChar.ToString()}(32)"),
                    Email = table.Column<string>($"{SqlDbType.VarChar.ToString()}(max)")
                },
                null,
                table =>
                {
                    table.PrimaryKey($"PK_{nameof(User)}", u => u.ID);
                });

            migrationBuilder.CreateTable(
                nameof(UserProfile),
                table => new
                {
                    ID = table.Column<long>(SqlDbType.Int.ToString()),
                    UserID = table.Column<long>(SqlDbType.Int.ToString()),
                    CreationDate = table.Column<DateTime>(SqlDbType.DateTime2.ToString()),
                    ModifiedDate = table.Column<DateTime>(SqlDbType.DateTime2.ToString()),
                    FirstName = table.Column<string>(SqlDbType.VarChar.ToString()),
                    MiddleName = table.Column<string>(SqlDbType.VarChar.ToString(), nullable: true),
                    LastName = table.Column<string>(SqlDbType.VarChar.ToString()),
                    Address1 = table.Column<string>(SqlDbType.VarChar.ToString()),
                    Address2 = table.Column<string>(SqlDbType.VarChar.ToString(), nullable: true),
                    City = table.Column<string>($"{SqlDbType.VarChar.ToString()}(100)"),
                    State = table.Column<string>($"{SqlDbType.Char.ToString()}(2)"),
                    ZipCode = table.Column<string>($"{SqlDbType.VarChar.ToString()}(10)")
                },
                null,
                table =>
                {
                    table.PrimaryKey($"PK_{nameof(UserProfile)}", u => u.ID);
                    table.ForeignKey(
                        $"FK_{nameof(UserProfile)}_{nameof(User)}",
                        c => c.UserID,
                        nameof(User),
                        "UserID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Migrating Down on {Class}", nameof(InitialMigration));

            migrationBuilder.DropTable(nameof(UserProfile));
            migrationBuilder.DropTable(nameof(User));
        }
    }
}

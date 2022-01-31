using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBAWebApplication.Migrations
{
    public partial class BillPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payee",
                columns: table => new
                {
                    PayeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payee", x => x.PayeeId);
                });

            migrationBuilder.CreateTable(
                name: "BillPay",
                columns: table => new
                {
                    BillPayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayeeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    ScheduleTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPay", x => x.BillPayId);
                    table.ForeignKey(
                        name: "FK_BillPay_Payee_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "Payee",
                        principalColumn: "PayeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillPay_PayeeId",
                table: "BillPay",
                column: "PayeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPay");

            migrationBuilder.DropTable(
                name: "Payee");
        }
    }
}

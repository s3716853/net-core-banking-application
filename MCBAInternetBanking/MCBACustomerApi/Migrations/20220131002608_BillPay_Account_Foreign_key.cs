using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBAWebApplication.Migrations
{
    public partial class BillPay_Account_Foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "BillPay",
                type: "nvarchar(4)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BillPay_AccountNumber",
                table: "BillPay",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPay_Account_AccountNumber",
                table: "BillPay",
                column: "AccountNumber",
                principalTable: "Account",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPay_Account_AccountNumber",
                table: "BillPay");

            migrationBuilder.DropIndex(
                name: "IX_BillPay_AccountNumber",
                table: "BillPay");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "BillPay",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBAWebApplication.Migrations
{
    public partial class BillPay_Completed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "BillPay",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "BillPay");
        }
    }
}

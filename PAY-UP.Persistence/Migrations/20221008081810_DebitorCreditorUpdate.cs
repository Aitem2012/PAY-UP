using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAY_UP.Persistence.Migrations
{
    public partial class DebitorCreditorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountOwed",
                table: "Debtors",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Debtors",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Installment",
                table: "Debtors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOwed",
                table: "Creditors",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Creditors",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Installment",
                table: "Creditors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOwed",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "Installment",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "AmountOwed",
                table: "Creditors");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Creditors");

            migrationBuilder.DropColumn(
                name: "Installment",
                table: "Creditors");
        }
    }
}

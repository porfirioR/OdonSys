using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Sql.Migrations;

public partial class UpdateTableInvoiceDetail : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_InvoiceDetailTeeth_InvoiceDetails_InvoiceDetailId",
            table: "InvoiceDetailTeeth");

        migrationBuilder.DropColumn(
            name: "Color",
            table: "InvoiceDetails");

        migrationBuilder.AddForeignKey(
            name: "FK_InvoiceDetailTeeth_InvoiceDetails_InvoiceDetailId",
            table: "InvoiceDetailTeeth",
            column: "InvoiceDetailId",
            principalTable: "InvoiceDetails",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_InvoiceDetailTeeth_InvoiceDetails_InvoiceDetailId",
            table: "InvoiceDetailTeeth");

        migrationBuilder.AddColumn<string>(
            name: "Color",
            table: "InvoiceDetails",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddForeignKey(
            name: "FK_InvoiceDetailTeeth_InvoiceDetails_InvoiceDetailId",
            table: "InvoiceDetailTeeth",
            column: "InvoiceDetailId",
            principalTable: "InvoiceDetails",
            principalColumn: "Id");
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sql.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_BillDetails_BillDetailId_BillDetailHeaderBillId_BillDetailClientProcedureId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BillDetailId_BillDetailHeaderBillId_BillDetailClientProcedureId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BillDetailClientProcedureId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BillDetailHeaderBillId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BillDetailId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ClientProcedures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ClientProcedures");

            migrationBuilder.AddColumn<int>(
                name: "ProcedurePrice",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcedurePrice",
                table: "BillDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "BillDetailClientProcedureId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillDetailHeaderBillId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillDetailId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ClientProcedures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ClientProcedures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillDetailId_BillDetailHeaderBillId_BillDetailClientProcedureId",
                table: "Payments",
                columns: new[] { "BillDetailId", "BillDetailHeaderBillId", "BillDetailClientProcedureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_BillDetails_BillDetailId_BillDetailHeaderBillId_BillDetailClientProcedureId",
                table: "Payments",
                columns: new[] { "BillDetailId", "BillDetailHeaderBillId", "BillDetailClientProcedureId" },
                principalTable: "BillDetails",
                principalColumns: new[] { "Id", "HeaderBillId", "ClientProcedureId" });
        }
    }
}

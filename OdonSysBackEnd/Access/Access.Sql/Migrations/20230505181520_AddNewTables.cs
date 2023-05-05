using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sql.Migrations
{
    public partial class AddNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeaderBills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Iva10 = table.Column<int>(type: "int", nullable: false),
                    TotalIva = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Timbrado = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeaderBills_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeaderBillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalPrice = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => new { x.Id, x.HeaderBillId, x.ClientProcedureId });
                    table.ForeignKey(
                        name: "FK_BillDetails_ClientProcedures_ClientProcedureId",
                        column: x => x.ClientProcedureId,
                        principalTable: "ClientProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_HeaderBills_HeaderBillId",
                        column: x => x.HeaderBillId,
                        principalTable: "HeaderBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeaderBillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillDetailClientProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillDetailHeaderBillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_BillDetails_BillDetailId_BillDetailHeaderBillId_BillDetailClientProcedureId",
                        columns: x => new { x.BillDetailId, x.BillDetailHeaderBillId, x.BillDetailClientProcedureId },
                        principalTable: "BillDetails",
                        principalColumns: new[] { "Id", "HeaderBillId", "ClientProcedureId" });
                    table.ForeignKey(
                        name: "FK_Payments_HeaderBills_HeaderBillId",
                        column: x => x.HeaderBillId,
                        principalTable: "HeaderBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ClientProcedureId",
                table: "BillDetails",
                column: "ClientProcedureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_HeaderBillId",
                table: "BillDetails",
                column: "HeaderBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HeaderBills_ClientId",
                table: "HeaderBills",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillDetailId_BillDetailHeaderBillId_BillDetailClientProcedureId",
                table: "Payments",
                columns: new[] { "BillDetailId", "BillDetailHeaderBillId", "BillDetailClientProcedureId" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_HeaderBillId",
                table: "Payments",
                column: "HeaderBillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "HeaderBills");
        }
    }
}

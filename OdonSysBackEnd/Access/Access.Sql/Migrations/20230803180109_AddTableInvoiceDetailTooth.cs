using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Sql.Migrations;

public partial class AddTableInvoiceDetailTooth : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_InvoiceDetails",
            table: "InvoiceDetails");

        migrationBuilder.DropIndex(
            name: "IX_InvoiceDetails_InvoiceId",
            table: "InvoiceDetails");

        migrationBuilder.AddColumn<string>(
            name: "Color",
            table: "InvoiceDetails",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "PK_InvoiceDetails",
            table: "InvoiceDetails",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "Teeth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Number = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                Jaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Quadrant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Teeth", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "InvoiceDetailTeeth",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ToothId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                InvoiceDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InvoiceDetailTeeth", x => x.Id);
                table.ForeignKey(
                    name: "FK_InvoiceDetailTeeth_InvoiceDetails_InvoiceDetailId",
                    column: x => x.InvoiceDetailId,
                    principalTable: "InvoiceDetails",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_InvoiceDetailTeeth_Teeth_ToothId",
                    column: x => x.ToothId,
                    principalTable: "Teeth",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_InvoiceDetails_InvoiceId_ClientProcedureId",
            table: "InvoiceDetails",
            columns: new[] { "InvoiceId", "ClientProcedureId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_InvoiceDetailTeeth_InvoiceDetailId",
            table: "InvoiceDetailTeeth",
            column: "InvoiceDetailId");

        migrationBuilder.CreateIndex(
            name: "IX_InvoiceDetailTeeth_ToothId",
            table: "InvoiceDetailTeeth",
            column: "ToothId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "InvoiceDetailTeeth");

        migrationBuilder.DropTable(
            name: "Teeth");

        migrationBuilder.DropPrimaryKey(
            name: "PK_InvoiceDetails",
            table: "InvoiceDetails");

        migrationBuilder.DropIndex(
            name: "IX_InvoiceDetails_InvoiceId_ClientProcedureId",
            table: "InvoiceDetails");

        migrationBuilder.DropColumn(
            name: "Color",
            table: "InvoiceDetails");

        migrationBuilder.AddPrimaryKey(
            name: "PK_InvoiceDetails",
            table: "InvoiceDetails",
            columns: new[] { "Id", "InvoiceId", "ClientProcedureId" });

        migrationBuilder.CreateIndex(
            name: "IX_InvoiceDetails_InvoiceId",
            table: "InvoiceDetails",
            column: "InvoiceId");
    }
}

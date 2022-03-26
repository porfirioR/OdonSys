using Microsoft.EntityFrameworkCore.Migrations;

namespace Sql.Migrations
{
    public partial class UpdateProcedureTooth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedureTeeth",
                table: "ProcedureTeeth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedureTeeth",
                table: "ProcedureTeeth",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureTeeth_ToothId_ProcedureId",
                table: "ProcedureTeeth",
                columns: new[] { "ToothId", "ProcedureId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedureTeeth",
                table: "ProcedureTeeth");

            migrationBuilder.DropIndex(
                name: "IX_ProcedureTeeth_ToothId_ProcedureId",
                table: "ProcedureTeeth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedureTeeth",
                table: "ProcedureTeeth",
                columns: new[] { "ToothId", "ProcedureId" });
        }
    }
}

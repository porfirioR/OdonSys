using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Sql.Migrations
{
    public partial class UpdateTableProcedureRenameColumnXRayToXRays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "XRay",
                table: "Procedures",
                newName: "XRays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "XRays",
                table: "Procedures",
                newName: "XRay");
        }
    }
}

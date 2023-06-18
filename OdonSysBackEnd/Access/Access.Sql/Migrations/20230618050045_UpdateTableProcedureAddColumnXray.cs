using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Sql.Migrations
{
    public partial class UpdateTableProcedureAddColumnXray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "XRay",
                table: "Procedures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XRay",
                table: "Procedures");
        }
    }
}

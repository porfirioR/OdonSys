using Microsoft.EntityFrameworkCore.Migrations;

namespace Sql.Migrations
{
    public partial class UpdateClientTableRenameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Clients",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "SecondLastName",
                table: "Clients",
                newName: "MiddleLastName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Clients",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "MiddleLastName",
                table: "Clients",
                newName: "SecondLastName");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sql.Migrations
{
    public partial class UpdateTableProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedSessions",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "Anesthesia",
                table: "ClientProcedures");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Procedures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Procedures");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedSessions",
                table: "Procedures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Anesthesia",
                table: "ClientProcedures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

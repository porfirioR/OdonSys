using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sql.Migrations
{
    public partial class UpdateBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Teeth",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Teeth",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "ProcedureTeeth",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "ProcedureTeeth",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Procedures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Procedures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "DoctorClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "DoctorClient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserUpdated",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Teeth");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Teeth");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "ProcedureTeeth");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "ProcedureTeeth");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "DoctorClient");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "DoctorClient");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserUpdated",
                table: "Clients");
        }
    }
}

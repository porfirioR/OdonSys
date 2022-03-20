using Microsoft.EntityFrameworkCore.Migrations;

namespace Sql.Migrations
{
    public partial class UpdateTeethTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Jaw",
                table: "Teeth",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quadrant",
                table: "Teeth",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jaw",
                table: "Teeth");

            migrationBuilder.DropColumn(
                name: "Quadrant",
                table: "Teeth");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Sql.Migrations
{
    public partial class UpdateTableFileStogareAddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "FileStorages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "FileStorages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FileStorages_Url",
                table: "FileStorages",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileStorages_Url",
                table: "FileStorages");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "FileStorages");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "FileStorages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

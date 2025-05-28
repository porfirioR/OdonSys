using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Access.Sql.Migrations;

/// <inheritdoc />
public partial class AddTableOrthodontic : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Orthodontics",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orthodontics", x => x.Id);
                table.ForeignKey(
                    name: "FK_Orthodontics_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Orthodontics_ClientId",
            table: "Orthodontics",
            column: "ClientId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orthodontics");
    }
}

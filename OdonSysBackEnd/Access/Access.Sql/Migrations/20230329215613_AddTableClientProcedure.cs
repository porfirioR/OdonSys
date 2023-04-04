using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sql.Migrations
{
    public partial class AddTableClientProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClient_Clients_ClientId",
                table: "UserClient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClient_Users_UserId",
                table: "UserClient");

            migrationBuilder.DropTable(
                name: "UserProcedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClient",
                table: "UserClient");

            migrationBuilder.RenameTable(
                name: "UserClient",
                newName: "UserClients");

            migrationBuilder.RenameIndex(
                name: "IX_UserClient_ClientId",
                table: "UserClients",
                newName: "IX_UserClients_ClientId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "UserClients",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "UserClients",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClients",
                table: "UserClients",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClientProcedures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Anesthesia = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientProcedures_UserClients_UserClientId",
                        column: x => x.UserClientId,
                        principalTable: "UserClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientProcedures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClients_Id_UserId_ClientId",
                table: "UserClients",
                columns: new[] { "Id", "UserId", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClients_UserId",
                table: "UserClients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProcedures_Id_ProcedureId_UserClientId",
                table: "ClientProcedures",
                columns: new[] { "Id", "ProcedureId", "UserClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientProcedures_ProcedureId",
                table: "ClientProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProcedures_UserClientId",
                table: "ClientProcedures",
                column: "UserClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProcedures_UserId",
                table: "ClientProcedures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClients_Clients_ClientId",
                table: "UserClients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClients_Users_UserId",
                table: "UserClients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClients_Clients_ClientId",
                table: "UserClients");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClients_Users_UserId",
                table: "UserClients");

            migrationBuilder.DropTable(
                name: "ClientProcedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClients",
                table: "UserClients");

            migrationBuilder.DropIndex(
                name: "IX_UserClients_Id_UserId_ClientId",
                table: "UserClients");

            migrationBuilder.DropIndex(
                name: "IX_UserClients_UserId",
                table: "UserClients");

            migrationBuilder.RenameTable(
                name: "UserClients",
                newName: "UserClient");

            migrationBuilder.RenameIndex(
                name: "IX_UserClients_ClientId",
                table: "UserClient",
                newName: "IX_UserClient_ClientId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "UserClient",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "UserClient",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClient",
                table: "UserClient",
                columns: new[] { "UserId", "ClientId" });

            migrationBuilder.CreateTable(
                name: "UserProcedures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserUpdated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProcedures", x => new { x.Id, x.ProcedureId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProcedures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProcedures_ProcedureId",
                table: "UserProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProcedures_UserId",
                table: "UserProcedures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClient_Clients_ClientId",
                table: "UserClient",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClient_Users_UserId",
                table: "UserClient",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class C2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollabEntityTable",
                columns: table => new
                {
                    CollabsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabEntityTable", x => x.CollabsId);
                    table.ForeignKey(
                        name: "FK_CollabEntityTable_NoteTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NoteTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollabEntityTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabEntityTable_NoteId",
                table: "CollabEntityTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CollabEntityTable_UserId",
                table: "CollabEntityTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabEntityTable");
        }
    }
}

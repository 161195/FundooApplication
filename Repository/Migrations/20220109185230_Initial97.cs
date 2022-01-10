using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Initial97 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LableTable",
                columns: table => new
                {
                    LableId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LableTable", x => x.LableId);
                    table.ForeignKey(
                        name: "FK_LableTable_NoteTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NoteTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LableTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LableTable_NoteId",
                table: "LableTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_LableTable_UserId",
                table: "LableTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LableTable");
        }
    }
}

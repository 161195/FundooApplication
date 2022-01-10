using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Initial98 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LableTable_NoteTable_NoteId",
                table: "LableTable");

            migrationBuilder.AlterColumn<long>(
                name: "NoteId",
                table: "LableTable",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_LableTable_NoteTable_NoteId",
                table: "LableTable",
                column: "NoteId",
                principalTable: "NoteTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LableTable_NoteTable_NoteId",
                table: "LableTable");

            migrationBuilder.AlterColumn<long>(
                name: "NoteId",
                table: "LableTable",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LableTable_NoteTable_NoteId",
                table: "LableTable",
                column: "NoteId",
                principalTable: "NoteTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalApiApp.Migrations
{
    /// <inheritdoc />
    public partial class IDsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Note",
                newName: "Studentid");

            migrationBuilder.RenameIndex(
                name: "IX_Note_StudentId",
                table: "Note",
                newName: "IX_Note_Studentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Student_Studentid",
                table: "Note",
                column: "Studentid",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Student_Studentid",
                table: "Note");

            migrationBuilder.RenameColumn(
                name: "Studentid",
                table: "Note",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_Studentid",
                table: "Note",
                newName: "IX_Note_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

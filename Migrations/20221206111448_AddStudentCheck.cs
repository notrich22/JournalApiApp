using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalApiApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Student_Studentid",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_UsersGroup_UsersGroupId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_UsersGroupId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "UsersGroupId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Student",
                newName: "Id");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Student_StudentId",
                table: "Note");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Student",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Note",
                newName: "Studentid");

            migrationBuilder.RenameIndex(
                name: "IX_Note_StudentId",
                table: "Note",
                newName: "IX_Note_Studentid");

            migrationBuilder.AddColumn<int>(
                name: "UsersGroupId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_UsersGroupId",
                table: "Student",
                column: "UsersGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Student_Studentid",
                table: "Note",
                column: "Studentid",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_UsersGroup_UsersGroupId",
                table: "Student",
                column: "UsersGroupId",
                principalTable: "UsersGroup",
                principalColumn: "Id");
        }
    }
}

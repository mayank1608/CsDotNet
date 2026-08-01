using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityDTO.Migrations
{
    /// <inheritdoc />
    public partial class updatedFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Departments",
                newName: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeptID",
                table: "Students",
                column: "DeptID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DeptID",
                table: "Students",
                column: "DeptID",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DeptID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeptID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Departments",
                newName: "Id");
        }
    }
}

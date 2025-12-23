using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workshop_1.Migrations
{
    /// <inheritdoc />
    public partial class OriginalFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeminarOriginalName",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SeminarOriginalName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SeminarOriginalName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SeminarOriginalName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 4L,
                column: "SeminarOriginalName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 5L,
                column: "SeminarOriginalName",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeminarOriginalName",
                table: "Enrollments");
        }
    }
}

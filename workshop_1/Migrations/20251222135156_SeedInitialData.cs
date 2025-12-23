using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace workshop_1.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AcquiredCredits", "CurrentSemester", "EducationLevel", "EnrollmentDate", "FirstName", "LastName", "StudentId" },
                values: new object[,]
                {
                    { 1L, null, 5, "Undergraduate", null, "Stefan", "Mitrev", "201001" },
                    { 2L, null, 3, "Undergraduate", null, "Marija", "Trajanova", "201002" },
                    { 3L, null, 7, "Undergraduate", null, "Nikola", "Petreski", "201003" },
                    { 4L, null, 1, "Undergraduate", null, "Sara", "Georgieva", "201004" },
                    { 5L, null, 5, "Undergraduate", null, "Daniel", "Ristov", "201005" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AcademicRank", "Degree", "FirstName", "HireDate", "LastName", "OfficeNumber" },
                values: new object[,]
                {
                    { 1, "Professor", "PhD", "Ivan", null, "Petrov", null },
                    { 2, "Associate Professor", "PhD", "Ana", null, "Stojanova", null },
                    { 3, "Assistant", "MSc", "Marko", null, "Iliev", null },
                    { 4, "Professor", "PhD", "Elena", null, "Nikolova", null },
                    { 5, "Assistant", "MSc", "Petar", null, "Kostov", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Credits", "EducationLevel", "FirstTeacherId", "Programme", "SecondTeacherId", "Semester", "Title" },
                values: new object[,]
                {
                    { 1, 6, null, 1, "Software Engineering", 2, 4, "Databases" },
                    { 2, 6, null, 2, "Software Engineering", 3, 5, "Web Programming" },
                    { 3, 6, null, 1, "Computer Science", 4, 3, "Algorithms" },
                    { 4, 6, null, 4, "Computer Science", 5, 4, "Operating Systems" },
                    { 5, 6, null, 1, "Software Engineering", 2, 6, "Software Design" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "AdditionalPoints", "CourseId", "ExamPoints", "FinishDate", "Grade", "ProjectPoints", "ProjectUrl", "Semester", "SeminarPoints", "SeminarUrl", "StudentId", "Year" },
                values: new object[,]
                {
                    { 1L, null, 1, null, null, 8, null, null, "Spring", null, null, 1L, 2024 },
                    { 2L, null, 2, null, null, 9, null, null, "Fall", null, null, 1L, 2024 },
                    { 3L, null, 1, null, null, 10, null, null, "Spring", null, null, 2L, 2024 },
                    { 4L, null, 3, null, null, 7, null, null, "Fall", null, null, 3L, 2024 },
                    { 5L, null, 4, null, null, 8, null, null, "Spring", null, null, 4L, 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

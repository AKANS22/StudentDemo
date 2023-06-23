using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollmentModels.Migrations
{
    /// <inheritdoc />
    public partial class SeededefaultRolesAndcourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45f2d6cd-c445-49d7-a220-39c1223947d1", null, "Administrator", "Administrator" },
                    { "5e6bb24e-2ea0-4151-a7cc-0fca6a2e807d", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "CourseId", "CreatedDate", "Credit", "Title", "createdBy", "updatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Minimal Api Developement", "", "" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "second Minimal Api Developement", "", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45f2d6cd-c445-49d7-a220-39c1223947d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e6bb24e-2ea0-4151-a7cc-0fca6a2e807d");

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "CourseId",
                keyValue: 2);
        }
    }
}

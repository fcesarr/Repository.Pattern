using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Pattern.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Deleted", "Name", "Updated" },
                values: new object[] { new Guid("bb18c456-c2db-4b60-bcf7-67f7f6a4c3fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xpto", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("bb18c456-c2db-4b60-bcf7-67f7f6a4c3fb"));
        }
    }
}

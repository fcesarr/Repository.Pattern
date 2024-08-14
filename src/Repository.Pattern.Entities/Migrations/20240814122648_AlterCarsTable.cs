using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Pattern.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AlterCarsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("bb18c456-c2db-4b60-bcf7-67f7f6a4c3fb"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deleted",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Deleted", "Name", "Updated" },
                values: new object[] { new Guid("241a30c1-3d70-40e8-a7e9-d5b83530bde8"), null, "Xpto", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("241a30c1-3d70-40e8-a7e9-d5b83530bde8"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deleted",
                table: "Cars",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Deleted", "Name", "Updated" },
                values: new object[] { new Guid("bb18c456-c2db-4b60-bcf7-67f7f6a4c3fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xpto", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}

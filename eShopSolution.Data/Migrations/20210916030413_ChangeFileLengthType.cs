using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"),
                column: "ConcurrencyStamp",
                value: "bd55a797-9dc7-4fa7-8c4c-8c274449955d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dfa2972b-0363-4947-b558-d8c349fcd083", "AQAAAAEAACcQAAAAEJHz2tVYjOquNTWarqa5sHlI/S8NqB60drgOOXabcKRVVsntEi8epzuGvX5b2vltSQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 16, 10, 4, 12, 906, DateTimeKind.Local).AddTicks(1511));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"),
                column: "ConcurrencyStamp",
                value: "5dabe6d5-c03f-4c3b-8ca8-f5528e6a2efc");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "230a3e87-022e-43cd-9404-4a4cc4ed6a6c", "AQAAAAEAACcQAAAAEOViufXbih0S6ejIwM99Ng9264TA4lzVFhKdo1hZaoeVj10JxF5AwIX/8bl2AUkESg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 16, 8, 28, 30, 951, DateTimeKind.Local).AddTicks(5614));
        }
    }
}

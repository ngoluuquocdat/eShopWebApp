using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 9, 43, 23, 559, DateTimeKind.Local).AddTicks(9781),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 8, 54, 39, 120, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"), "6b1af01b-eaee-43a3-8e06-2b4a8544101c", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"), new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"), 0, "0f9a528f-3ac8-481f-b00e-3b970ddce978", new DateTime(2000, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "ngoluuquocdat@gmail.com", true, "Quốc Đạt", "Ngô Lưu", false, null, "ngoluuquocdat@gmail.com", "admin", "AQAAAAEAACcQAAAAED7TAcnpvfu97lHEKuUYA4eEDtUL1yNJcKRU8JJJ6MhT7iHzOKDpRcijCSKTf7GTWA==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Deep cut sleeveless shirt, oversized formed");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 9, 43, 23, 577, DateTimeKind.Local).AddTicks(2444));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"), new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 8, 54, 39, 120, DateTimeKind.Local).AddTicks(149),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 9, 43, 23, 559, DateTimeKind.Local).AddTicks(9781));

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Deep cut sleeveless shirt, oversized from");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 8, 54, 39, 136, DateTimeKind.Local).AddTicks(7331));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AspNetCoreIdentityDatabase_updateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 8, 54, 39, 120, DateTimeKind.Local).AddTicks(149),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 8, 35, 38, 5, DateTimeKind.Local).AddTicks(1789));

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Áo sát nách khoét sâu, form rộng");

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "SeoTitle" },
                values: new object[] { "Deep cut sleeveless shirt, oversized from", "Sleeveless shirt for men gymmer" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 8, 54, 39, 136, DateTimeKind.Local).AddTicks(7331));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 8, 35, 38, 5, DateTimeKind.Local).AddTicks(1789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 8, 54, 39, 120, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "SeoTitle" },
                values: new object[] { "", "Deep cut sleeveless shirt, oversized from" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 8, 35, 38, 22, DateTimeKind.Local).AddTicks(402));
        }
    }
}

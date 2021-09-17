using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 9, 15, 9, 43, 23, 559, DateTimeKind.Local).AddTicks(9781));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 15, 9, 43, 23, 559, DateTimeKind.Local).AddTicks(9781),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"),
                column: "ConcurrencyStamp",
                value: "6b1af01b-eaee-43a3-8e06-2b4a8544101c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f9a528f-3ac8-481f-b00e-3b970ddce978", "AQAAAAEAACcQAAAAED7TAcnpvfu97lHEKuUYA4eEDtUL1yNJcKRU8JJJ6MhT7iHzOKDpRcijCSKTf7GTWA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 9, 43, 23, 577, DateTimeKind.Local).AddTicks(2444));
        }
    }
}

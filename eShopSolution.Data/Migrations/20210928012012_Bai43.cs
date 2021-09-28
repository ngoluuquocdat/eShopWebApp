using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class Bai43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"),
                column: "ConcurrencyStamp",
                value: "6b1c85e4-3dc3-402c-8fee-b7925b6e657d");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("c4e9559f-505b-44c8-b1ba-1701739b6ae7"), "26b37d64-8bff-4226-bce9-297436173db0", "Normal User role", "user", "user" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "16f77ac0-d99c-41cd-b49f-5b8d4a071c1b", "AQAAAAEAACcQAAAAEAhyKYjwKtCzTpVruGXDlH2gfBh3HAbH3l8Lo+ieJnAPmtb/oULN0Ck8cOB7qA0kGw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 28, 8, 20, 11, 786, DateTimeKind.Local).AddTicks(1873));

            migrationBuilder.InsertData(
                table: "Slides",
                columns: new[] { "Id", "Description", "Image", "Name", "SortOrder", "Status", "Url" },
                values: new object[,]
                {
                    { 1, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/1.png", "Second Thumbnail label", 1, 1, "#" },
                    { 2, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/2.png", "Second Thumbnail label", 2, 1, "#" },
                    { 3, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/3.png", "Second Thumbnail label", 3, 1, "#" },
                    { 4, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/4.png", "Second Thumbnail label", 4, 1, "#" },
                    { 5, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/5.png", "Second Thumbnail label", 5, 1, "#" },
                    { 6, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/6.png", "Second Thumbnail label", 6, 1, "#" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4e9559f-505b-44c8-b1ba-1701739b6ae7"));

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5810934d-2e2a-4e8f-abe6-a4f1ea58fe37"),
                column: "ConcurrencyStamp",
                value: "09716b13-8e79-4029-8730-4c2b3e933f0a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9898841-3761-47f9-8ed3-eba4a62b2fe5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "238390a7-30db-4415-b936-dc0195a0ffaa", "AQAAAAEAACcQAAAAEFIILT58unRz0aAnrfaj5tYuLoWcWEEdcHuKwca1wAX0UiUKdbC22Dnn2uwT/DfCew==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 9, 27, 15, 50, 26, 177, DateTimeKind.Local).AddTicks(4173));
        }
    }
}

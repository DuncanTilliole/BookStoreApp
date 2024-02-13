using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedsusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "067ce6cb-d8ba-4260-a6ab-e37a11ecee5d", null, "Admin", "ADMIN" },
                    { "1514ed7e-2074-4e14-b2bc-a5827ade00eb", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2c155adc-7584-40ba-a57d-99872511460e", 0, "39365c0b-42c6-48d9-a27a-afd9a69bdc30", "admin@gmail.com", false, "System", "Admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEFumQXGaBsL9vMM7uxY8qLx/uvBvIfJPP5pfkTqwI0T7i0+lTSvWxRQH55Pv7Gz1zg==", null, false, "ae9d36df-be4d-4b80-a31b-ed44318aa23a", false, "admin@gmail.com" },
                    { "4063871d-9c55-47b5-93e7-22c4c92d2bae", 0, "d97032d2-1d15-4b8e-be89-af6fa5012212", "john.doe@gmail.com", false, "John", "Doe", false, null, "JOHN.DOE@GMAIL.COM", "JOHN.DOE@GMAIL.COM", "AQAAAAIAAYagAAAAEPb0YJcKJ6e5xRPVAjH/zQxoT5ECGU3yLjZBZUCE9c3qE/HHZB/f7/CDOUzNldlThw==", null, false, "79aae713-549a-4e4d-ba57-02d7b2e2ee3e", false, "john.doe@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "067ce6cb-d8ba-4260-a6ab-e37a11ecee5d", "2c155adc-7584-40ba-a57d-99872511460e" },
                    { "1514ed7e-2074-4e14-b2bc-a5827ade00eb", "4063871d-9c55-47b5-93e7-22c4c92d2bae" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "067ce6cb-d8ba-4260-a6ab-e37a11ecee5d", "2c155adc-7584-40ba-a57d-99872511460e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1514ed7e-2074-4e14-b2bc-a5827ade00eb", "4063871d-9c55-47b5-93e7-22c4c92d2bae" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "067ce6cb-d8ba-4260-a6ab-e37a11ecee5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1514ed7e-2074-4e14-b2bc-a5827ade00eb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c155adc-7584-40ba-a57d-99872511460e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4063871d-9c55-47b5-93e7-22c4c92d2bae");
        }
    }
}

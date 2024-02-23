using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c155adc-7584-40ba-a57d-99872511460e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c966219-1bf3-4253-ba3e-ebcf992d9fbd", "AQAAAAIAAYagAAAAEJU6Quw+A8JajSePuKVmXDGRJehWQoF/cGwhwtAVf3lHNdarzyEUg3wi8o12zxCFzQ==", "c7a70f53-09c1-49b2-a4cf-06d3de13d0b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4063871d-9c55-47b5-93e7-22c4c92d2bae",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26f1509f-a53f-4bbc-a41b-4ac8aa60ecd2", "AQAAAAIAAYagAAAAEFkz945/+C1dOwVIlfD0BQmcwDNjYPs4IvKtWd6DgecMuQO7n2x2snpvmHYyp6oGiQ==", "9b8bba1e-b582-41a1-9949-b99a9fbe0893" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c155adc-7584-40ba-a57d-99872511460e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ac0d3d0-cdf8-4471-a172-8212f1836c73", "AQAAAAIAAYagAAAAEFlHCRWhmJDfhRbEJ4nFkbWZyxzl0X6Zh5TpKOYTDEJTXIJujCMNe1NrWrogyz5AMw==", "c5c774e4-dcca-4840-8dd7-b4a1e84716c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4063871d-9c55-47b5-93e7-22c4c92d2bae",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2cfe4ba-11d2-465b-9939-0f261ddd7037", "AQAAAAIAAYagAAAAEDn87dLd49wu/ykRmPypjGqEcyAich1+Xyk+07Hbfw71+Ppq9TBIfOgr3s2QYr+TaQ==", "eb50b4b0-0715-4db9-82c3-4e08d6e2a540" });
        }
    }
}

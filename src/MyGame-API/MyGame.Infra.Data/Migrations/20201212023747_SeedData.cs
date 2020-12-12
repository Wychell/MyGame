using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyGame.Infra.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "CreateDate", "Gender", "Name" },
                values: new object[] { new Guid("21eaa1fa-bdf4-496a-85ac-ce21da56cff9"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Esporte", "FIFA 21" });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "CreateDate", "Gender", "Name" },
                values: new object[] { new Guid("7eef25b8-ffd4-4b59-b183-edf36dc33d2a"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aventura", "GTA V" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateDate", "Email", "Name", "Password" },
                values: new object[] { new Guid("7b154c78-d523-48f9-b0b0-7b9b9e9cc65f"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "mychell.mds@gmail.com", "Mychell", "password" });

            migrationBuilder.InsertData(
                table: "Friend",
                columns: new[] { "Id", "CreateDate", "Email", "Name", "Phone", "UserId" },
                values: new object[] { new Guid("603c8ac5-2fc5-491f-9fb4-0924320b4301"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "amigo@gmail.com", "Amigo da Onça", "333333", new Guid("7b154c78-d523-48f9-b0b0-7b9b9e9cc65f") });

            migrationBuilder.InsertData(
                table: "Friend",
                columns: new[] { "Id", "CreateDate", "Email", "Name", "Phone", "UserId" },
                values: new object[] { new Guid("dfb45e41-045d-4fdd-a408-2bba3d98b890"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "cobra@gmail.com", "Amigo da Cobra", "444444", new Guid("7b154c78-d523-48f9-b0b0-7b9b9e9cc65f") });

            migrationBuilder.InsertData(
                table: "Loan",
                columns: new[] { "Id", "CreateDate", "EndDate", "FriendId", "GameId" },
                values: new object[] { new Guid("a7394a7a-358c-4d30-ac79-86725ce2cbb0"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("603c8ac5-2fc5-491f-9fb4-0924320b4301"), new Guid("21eaa1fa-bdf4-496a-85ac-ce21da56cff9") });

            migrationBuilder.InsertData(
                table: "Loan",
                columns: new[] { "Id", "CreateDate", "EndDate", "FriendId", "GameId" },
                values: new object[] { new Guid("4cc3e40d-b70e-4258-997b-c51abe6b26fc"), new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("dfb45e41-045d-4fdd-a408-2bba3d98b890"), new Guid("7eef25b8-ffd4-4b59-b183-edf36dc33d2a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: new Guid("4cc3e40d-b70e-4258-997b-c51abe6b26fc"));

            migrationBuilder.DeleteData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: new Guid("a7394a7a-358c-4d30-ac79-86725ce2cbb0"));

            migrationBuilder.DeleteData(
                table: "Friend",
                keyColumn: "Id",
                keyValue: new Guid("603c8ac5-2fc5-491f-9fb4-0924320b4301"));

            migrationBuilder.DeleteData(
                table: "Friend",
                keyColumn: "Id",
                keyValue: new Guid("dfb45e41-045d-4fdd-a408-2bba3d98b890"));

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "Id",
                keyValue: new Guid("21eaa1fa-bdf4-496a-85ac-ce21da56cff9"));

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "Id",
                keyValue: new Guid("7eef25b8-ffd4-4b59-b183-edf36dc33d2a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("7b154c78-d523-48f9-b0b0-7b9b9e9cc65f"));
        }
    }
}

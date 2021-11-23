using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApIHotelListing.Migrations
{
    public partial class AddedDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5673c5ff-b121-4e2f-8bcf-b391afbd718d", "e6b2a549-3a05-4f3e-b002-65e0f5dd3d14", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c07c927b-f151-4b9a-ba93-8e41fbb12f73", "7bb14e8f-adc6-407d-9e40-89d6bd45e9cb", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5673c5ff-b121-4e2f-8bcf-b391afbd718d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c07c927b-f151-4b9a-ba93-8e41fbb12f73");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_22.Server.Migrations
{
    public partial class adduserinfoinorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Orders");
        }
    }
}

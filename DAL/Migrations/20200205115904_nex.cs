using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class nex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "public",
                table: "t_account",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "public",
                table: "t_account");
        }
    }
}

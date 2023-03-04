using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreDataAccess.Migrations
{
    public partial class AddEmailToOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderHeaders");
        }
    }
}

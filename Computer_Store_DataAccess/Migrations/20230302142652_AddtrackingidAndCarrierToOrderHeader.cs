using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreDataAccess.Migrations
{
    public partial class AddtrackingidAndCarrierToOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tracking",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Tracking",
                table: "OrderHeaders");
        }
    }
}

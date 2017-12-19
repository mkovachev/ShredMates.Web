using Microsoft.EntityFrameworkCore.Migrations;

namespace ShredMates.Data.Migrations
{
    public partial class ProductDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Products",
                newName: "DateCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Products",
                newName: "CreatedDate");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Product",
                newName: "Describe");

            migrationBuilder.RenameColumn(
                name: "NumberOfViews",
                table: "Product",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Purchases",
                table: "Product",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purchases",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Product",
                newName: "NumberOfViews");

            migrationBuilder.RenameColumn(
                name: "Describe",
                table: "Product",
                newName: "Title");
        }
    }
}

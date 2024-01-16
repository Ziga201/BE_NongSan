using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class updateorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualPrice",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "OriginalPrice",
                table: "Order",
                newName: "TotalPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order",
                newName: "OriginalPrice");

            migrationBuilder.AddColumn<double>(
                name: "ActualPrice",
                table: "Order",
                type: "float",
                nullable: true);
        }
    }
}

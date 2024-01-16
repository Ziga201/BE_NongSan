using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class updateblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Blog",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "Blog");
        }
    }
}

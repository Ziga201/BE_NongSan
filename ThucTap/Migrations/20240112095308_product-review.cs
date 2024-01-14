using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class productreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentRated",
                table: "ProductReview");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "ProductReview");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ProductReview",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ContentSeen",
                table: "ProductReview",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ProductReview",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ProductReview",
                newName: "ContentSeen");

            migrationBuilder.AddColumn<string>(
                name: "ContentRated",
                table: "ProductReview",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "ProductReview",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

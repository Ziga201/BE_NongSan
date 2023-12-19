﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTap.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageTypeProduct",
                table: "ProductType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageTypeProduct",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

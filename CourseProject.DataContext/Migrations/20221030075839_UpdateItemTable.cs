using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.DataContext.Migrations
{
    public partial class UpdateItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "UsersWhoLiked",
                table: "Items",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UsersWhoLiked",
                table: "Items");
        }
    }
}

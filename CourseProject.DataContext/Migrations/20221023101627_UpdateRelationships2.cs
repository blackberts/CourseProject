using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.DataContext.Migrations
{
    public partial class UpdateRelationships2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CollectionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "CollectionItem",
                columns: table => new
                {
                    CollectionsCollectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemsItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItem", x => new { x.CollectionsCollectionId, x.ItemsItemId });
                    table.ForeignKey(
                        name: "FK_CollectionItem_Collections_CollectionsCollectionId",
                        column: x => x.CollectionsCollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItem_Items_ItemsItemId",
                        column: x => x.ItemsItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_ItemsItemId",
                table: "CollectionItem",
                column: "ItemsItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItem");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "Items",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId",
                table: "Items",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "CollectionId");
        }
    }
}

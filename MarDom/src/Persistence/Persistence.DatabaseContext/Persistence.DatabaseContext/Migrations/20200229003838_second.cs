using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.DatabaseContext.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "MovementArticles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MovementArticles_WarehouseId",
                table: "MovementArticles",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovementArticles_Warehouses_WarehouseId",
                table: "MovementArticles",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovementArticles_Warehouses_WarehouseId",
                table: "MovementArticles");

            migrationBuilder.DropIndex(
                name: "IX_MovementArticles_WarehouseId",
                table: "MovementArticles");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "MovementArticles");
        }
    }
}

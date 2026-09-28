using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMello.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProductWareHouseModeland_SubSku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationCodeId",
                table: "SubSkus");

            migrationBuilder.DropIndex(
                name: "IX_SubSkus_LocationCodeId",
                table: "SubSkus");

            migrationBuilder.DropColumn(
                name: "LocationCodeId",
                table: "SubSkus");

            migrationBuilder.CreateIndex(
                name: "IX_SubSkus_LocationId",
                table: "SubSkus",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationId",
                table: "SubSkus",
                column: "LocationId",
                principalTable: "WarehouseLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationId",
                table: "SubSkus");

            migrationBuilder.DropIndex(
                name: "IX_SubSkus_LocationId",
                table: "SubSkus");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationCodeId",
                table: "SubSkus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubSkus_LocationCodeId",
                table: "SubSkus",
                column: "LocationCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationCodeId",
                table: "SubSkus",
                column: "LocationCodeId",
                principalTable: "WarehouseLocations",
                principalColumn: "Id");
        }
    }
}

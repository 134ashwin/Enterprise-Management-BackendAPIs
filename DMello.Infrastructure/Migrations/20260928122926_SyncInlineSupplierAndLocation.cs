using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMello.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncInlineSupplierAndLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationId",
                table: "SubSkus");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "SubSkus",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationId",
                table: "SubSkus",
                column: "LocationId",
                principalTable: "WarehouseLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationId",
                table: "SubSkus");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "SubSkus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationId",
                table: "SubSkus",
                column: "LocationId",
                principalTable: "WarehouseLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

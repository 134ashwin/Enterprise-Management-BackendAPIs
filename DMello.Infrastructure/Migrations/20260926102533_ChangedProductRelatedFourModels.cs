using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMello.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProductRelatedFourModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSkus_WarehouseLocations_RackCodeId",
                table: "SubSkus");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "SubSkus");

            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "SubSkus");

            migrationBuilder.RenameColumn(
                name: "RackCode",
                table: "WarehouseLocations",
                newName: "LocationCode");

            migrationBuilder.RenameColumn(
                name: "RackCodeId",
                table: "SubSkus",
                newName: "LocationCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubSkus_RackCodeId",
                table: "SubSkus",
                newName: "IX_SubSkus_LocationCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationCodeId",
                table: "SubSkus",
                column: "LocationCodeId",
                principalTable: "WarehouseLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSkus_WarehouseLocations_LocationCodeId",
                table: "SubSkus");

            migrationBuilder.RenameColumn(
                name: "LocationCode",
                table: "WarehouseLocations",
                newName: "RackCode");

            migrationBuilder.RenameColumn(
                name: "LocationCodeId",
                table: "SubSkus",
                newName: "RackCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubSkus_LocationCodeId",
                table: "SubSkus",
                newName: "IX_SubSkus_RackCodeId");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SubSkus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "SubSkus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSkus_WarehouseLocations_RackCodeId",
                table: "SubSkus",
                column: "RackCodeId",
                principalTable: "WarehouseLocations",
                principalColumn: "Id");
        }
    }
}

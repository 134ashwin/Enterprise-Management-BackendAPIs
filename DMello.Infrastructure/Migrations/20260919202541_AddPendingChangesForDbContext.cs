using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMello.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingChangesForDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ Safe execution: Checks sys.indexes before attempting drop
            migrationBuilder.Sql(@"
        IF EXISTS (
            SELECT 1 
            FROM sys.indexes 
            WHERE name = 'IX_SalesOrders_OrderNo' 
              AND object_id = OBJECT_ID('SalesOrders')
        )
        BEGIN
            DROP INDEX IX_SalesOrders_OrderNo ON SalesOrders;
        END
    ");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_OrderNo",
                table: "SalesOrders",
                column: "OrderNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_OrderNo",
                table: "SalesOrders");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_OrderNo",
                table: "SalesOrders",
                column: "OrderNo",
                unique: true);
        }
    }
}

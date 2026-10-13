using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMello.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressAndDescriptionToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Customers");
        }
    }
}

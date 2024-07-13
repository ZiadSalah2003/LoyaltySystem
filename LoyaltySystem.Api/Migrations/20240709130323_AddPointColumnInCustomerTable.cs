using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltySystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPointColumnInCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "Customers");
        }
    }
}

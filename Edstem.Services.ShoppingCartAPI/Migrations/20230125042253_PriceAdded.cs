using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edstem.Services.ShoppingCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class PriceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "CartDetails",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartDetails");
        }
    }
}

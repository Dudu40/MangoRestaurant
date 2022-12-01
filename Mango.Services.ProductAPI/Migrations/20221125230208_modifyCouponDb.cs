using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.API.Migrations
{
    /// <inheritdoc />
    public partial class modifyCouponDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coupons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

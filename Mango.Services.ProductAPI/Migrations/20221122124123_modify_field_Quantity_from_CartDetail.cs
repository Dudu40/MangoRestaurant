using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.API.Migrations
{
    /// <inheritdoc />
    public partial class modifyfieldQuantityfromCartDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "CartDetails",
                newName: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartDetails",
                newName: "Count");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CartHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

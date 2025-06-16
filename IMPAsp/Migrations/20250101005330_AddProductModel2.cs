using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMPAsp.Migrations
{
    /// <inheritdoc />
    public partial class AddProductModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Proudcts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proudcts",
                table: "Proudcts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Proudcts",
                table: "Proudcts");

            migrationBuilder.RenameTable(
                name: "Proudcts",
                newName: "Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");
        }
    }
}

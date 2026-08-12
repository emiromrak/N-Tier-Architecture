using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductIds",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIds",
                table: "Orders");
        }
    }
}

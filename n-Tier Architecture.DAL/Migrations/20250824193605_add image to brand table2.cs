using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace n_Tier_Architecture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addimagetobrandtable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Brands");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bonsai_webshop.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UrlSlugToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlSlug",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlSlug",
                table: "Products");
        }
    }
}

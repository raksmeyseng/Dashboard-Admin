using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchtistStudio.Migrations
{
    /// <inheritdoc />
    public partial class EntityAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePathPlanning",
                table: "About",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Planning",
                table: "About",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePathPlanning",
                table: "About");

            migrationBuilder.DropColumn(
                name: "Planning",
                table: "About");
        }
    }
}

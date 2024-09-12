using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchtistStudio.Migrations
{
    /// <inheritdoc />
    public partial class I : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Social_Contact_ContactId",
                table: "Social");

            migrationBuilder.DropIndex(
                name: "IX_Social_ContactId",
                table: "Social");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Social");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Social",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Social_ContactId",
                table: "Social",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Social_Contact_ContactId",
                table: "Social",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

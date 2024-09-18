using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchtistStudio.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAEP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Architecture_Category_CategoryId",
                table: "Architecture");

            migrationBuilder.DropForeignKey(
                name: "FK_Architecture_Project_ProjectId",
                table: "Architecture");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineeing_Category_CategoryId",
                table: "Engineeing");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineeing_Project_CategoryId",
                table: "Engineeing");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Project_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Architecture_ProjectId",
                table: "Architecture");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Product",
                newName: "CategoryProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryProductId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Engineeing",
                newName: "CategoryEngineeringId");

            migrationBuilder.RenameIndex(
                name: "IX_Engineeing_CategoryId",
                table: "Engineeing",
                newName: "IX_Engineeing_CategoryEngineeringId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Architecture",
                newName: "CategoryArchitectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Architecture_CategoryId",
                table: "Architecture",
                newName: "IX_Architecture_CategoryArchitectureId");

            migrationBuilder.CreateTable(
                name: "CategoryArchitecture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    InActive = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryArchitecture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEngineering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    InActive = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEngineering", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    InActive = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Architecture_CategoryArchitecture_CategoryArchitectureId",
                table: "Architecture",
                column: "CategoryArchitectureId",
                principalTable: "CategoryArchitecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Architecture_Project_CategoryArchitectureId",
                table: "Architecture",
                column: "CategoryArchitectureId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeing_CategoryEngineering_CategoryEngineeringId",
                table: "Engineeing",
                column: "CategoryEngineeringId",
                principalTable: "CategoryEngineering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeing_Project_CategoryEngineeringId",
                table: "Engineeing",
                column: "CategoryEngineeringId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CategoryProduct_CategoryProductId",
                table: "Product",
                column: "CategoryProductId",
                principalTable: "CategoryProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Project_CategoryProductId",
                table: "Product",
                column: "CategoryProductId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Architecture_CategoryArchitecture_CategoryArchitectureId",
                table: "Architecture");

            migrationBuilder.DropForeignKey(
                name: "FK_Architecture_Project_CategoryArchitectureId",
                table: "Architecture");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineeing_CategoryEngineering_CategoryEngineeringId",
                table: "Engineeing");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineeing_Project_CategoryEngineeringId",
                table: "Engineeing");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CategoryProduct_CategoryProductId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Project_CategoryProductId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "CategoryArchitecture");

            migrationBuilder.DropTable(
                name: "CategoryEngineering");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoryProductId",
                table: "Product",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryProductId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryEngineeringId",
                table: "Engineeing",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Engineeing_CategoryEngineeringId",
                table: "Engineeing",
                newName: "IX_Engineeing_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryArchitectureId",
                table: "Architecture",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Architecture_CategoryArchitectureId",
                table: "Architecture",
                newName: "IX_Architecture_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Architecture_ProjectId",
                table: "Architecture",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Architecture_Category_CategoryId",
                table: "Architecture",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Architecture_Project_ProjectId",
                table: "Architecture",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeing_Category_CategoryId",
                table: "Engineeing",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineeing_Project_CategoryId",
                table: "Engineeing",
                column: "CategoryId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Project_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

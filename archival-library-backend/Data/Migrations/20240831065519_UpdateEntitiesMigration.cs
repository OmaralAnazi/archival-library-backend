using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace archival_library_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "DocumentMetadatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentMetadatas_CategoryId",
                table: "DocumentMetadatas",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentMetadatas_Category_CategoryId",
                table: "DocumentMetadatas",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentMetadatas_Category_CategoryId",
                table: "DocumentMetadatas");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_DocumentMetadatas_CategoryId",
                table: "DocumentMetadatas");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "DocumentMetadatas");
        }
    }
}

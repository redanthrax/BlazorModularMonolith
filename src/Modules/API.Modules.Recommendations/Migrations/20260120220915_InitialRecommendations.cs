using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Modules.Recommendations.Migrations
{
    /// <inheritdoc />
    public partial class InitialRecommendations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceComicId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecommendedComicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Score = table.Column<double>(type: "double precision", precision: 5, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_RecommendedComicId",
                table: "Recommendations",
                column: "RecommendedComicId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_SourceComicId",
                table: "Recommendations",
                column: "SourceComicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recommendations");
        }
    }
}

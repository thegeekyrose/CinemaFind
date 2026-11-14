using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaFind.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddViewingHistoryUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ViewingHistories_UserId",
                table: "ViewingHistories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ViewingHistory_Progress",
                table: "ViewingHistories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserRating_Score",
                table: "UserRatings");

            migrationBuilder.CreateIndex(
                name: "IX_ViewingHistories_UserId_FilmId",
                table: "ViewingHistories",
                columns: new[] { "UserId", "FilmId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ViewingHistories_UserId_FilmId",
                table: "ViewingHistories");

            migrationBuilder.CreateIndex(
                name: "IX_ViewingHistories_UserId",
                table: "ViewingHistories",
                column: "UserId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ViewingHistory_Progress",
                table: "ViewingHistories",
                sql: "[Progress] IS NULL OR ([Progress] >= 0 AND [Progress] <= 100)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserRating_Score",
                table: "UserRatings",
                sql: "[Score] >= 1 AND [Score] <= 10");
        }
    }
}

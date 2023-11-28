using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Movie",
                table: "Movies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Movie",
                table: "Movies",
                column: "Movie");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Accounts_Movie",
                table: "Movies",
                column: "Movie",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Accounts_Movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Movie",
                table: "Movies");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestServer.Migrations
{
    /// <inheritdoc />
    public partial class LikeACommentMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NumberOfLikes",
                table: "Comments",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfLikes",
                table: "Comments");
        }
    }
}

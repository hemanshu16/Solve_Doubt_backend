using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Discussion_Forum.Migrations
{
    /// <inheritdoc />
    public partial class upvoted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "questionid",
                table: "Upvote_",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "questionid",
                table: "Upvote_");
        }
    }
}

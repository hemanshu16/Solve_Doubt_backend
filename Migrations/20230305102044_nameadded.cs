using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Discussion_Forum.Migrations
{
    /// <inheritdoc />
    public partial class nameadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Questions_",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "Questions_");
        }
    }
}

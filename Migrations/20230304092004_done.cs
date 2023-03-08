using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Discussion_Forum.Migrations
{
    /// <inheritdoc />
    public partial class done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag__Questions__QuestionsId",
                table: "Tag_");

            migrationBuilder.DropIndex(
                name: "IX_Tag__QuestionsId",
                table: "Tag_");

            migrationBuilder.DropColumn(
                name: "QuestionsId",
                table: "Tag_");

            migrationBuilder.AddColumn<long>(
                name: "question_id",
                table: "Tag_",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "question_id",
                table: "Tag_");

            migrationBuilder.AddColumn<long>(
                name: "QuestionsId",
                table: "Tag_",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag__QuestionsId",
                table: "Tag_",
                column: "QuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag__Questions__QuestionsId",
                table: "Tag_",
                column: "QuestionsId",
                principalTable: "Questions_",
                principalColumn: "Id");
        }
    }
}

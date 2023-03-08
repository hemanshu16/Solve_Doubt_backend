using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Discussion_Forum.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers_",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_id = table.Column<long>(type: "bigint", nullable: true),
                    User_id = table.Column<long>(type: "bigint", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Upvotes = table.Column<int>(type: "int", nullable: true),
                    Update_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions_",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag_",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tag_Name = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers_");

            migrationBuilder.DropTable(
                name: "Questions_");

            migrationBuilder.DropTable(
                name: "Tag_");

            migrationBuilder.DropTable(
                name: "User_");
        }
    }
}

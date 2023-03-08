using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Discussion_Forum.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User_",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User_",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "User_",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "User_",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "User_");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "User_");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User_",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User_",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

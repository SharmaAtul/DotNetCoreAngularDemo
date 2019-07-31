using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularDemo.Migrations
{
    public partial class modelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Models",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Makes",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Models",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Makes",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}

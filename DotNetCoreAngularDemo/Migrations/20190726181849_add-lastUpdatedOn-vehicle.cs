using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularDemo.Migrations
{
    public partial class addlastUpdatedOnvehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Vehicles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Vehicles");
        }
    }
}

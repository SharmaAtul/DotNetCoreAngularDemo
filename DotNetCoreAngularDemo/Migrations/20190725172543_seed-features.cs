using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularDemo.Migrations
{
    public partial class seedfeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Features (Name) values ('Feature1')");
            migrationBuilder.Sql("Insert into Features (Name) values ('Feature2')");
            migrationBuilder.Sql("Insert into Features (Name) values ('Feature3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Features where Name in ('Feature1','Feature2','Feature3')");
        }
    }
}

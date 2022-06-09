using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_education : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "University_Id",
                table: "Educations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "University_Id",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

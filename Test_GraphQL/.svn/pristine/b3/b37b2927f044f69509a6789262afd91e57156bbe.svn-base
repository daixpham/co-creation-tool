using Microsoft.EntityFrameworkCore.Migrations;

namespace graphQL_test.Migrations
{
    public partial class addNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Author_Id",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author_Id",
                table: "Posts");
        }
    }
}

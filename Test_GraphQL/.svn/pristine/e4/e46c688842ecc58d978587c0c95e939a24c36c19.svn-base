using Microsoft.EntityFrameworkCore.Migrations;

namespace graphQL_test.Migrations
{
    public partial class changeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author_Id",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Author_Id",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

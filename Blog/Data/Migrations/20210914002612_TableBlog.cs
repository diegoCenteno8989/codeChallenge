using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class TableBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    blogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    blogTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    blogDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    blogUrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.blogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}

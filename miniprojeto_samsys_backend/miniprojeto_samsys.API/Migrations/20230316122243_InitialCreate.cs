using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojeto_samsys.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorName__AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookName__BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookPrice__BookPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookAuthorID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_BookAuthorID",
                        column: x => x.BookAuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookAuthorID",
                table: "Books",
                column: "BookAuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}

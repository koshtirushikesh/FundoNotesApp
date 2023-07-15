using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Collaboration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaboration",
                columns: table => new
                {
                    CollaborationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationEmail = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    NoteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaboration", x => x.CollaborationID);
                    table.ForeignKey(
                        name: "FK_Collaboration_Note_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Note",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Collaboration_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_NoteID",
                table: "Collaboration",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_UserID",
                table: "Collaboration",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaboration");
        }
    }
}

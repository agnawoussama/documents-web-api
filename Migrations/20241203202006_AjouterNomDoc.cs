using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace documents_web_api.Migrations
{
    public partial class AjouterNomDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nom",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nom",
                table: "Documents");
        }
    }
}

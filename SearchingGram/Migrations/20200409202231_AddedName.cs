using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class AddedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Watchers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Monitors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Watchers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Monitors");
        }
    }
}

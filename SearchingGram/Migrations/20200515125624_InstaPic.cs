using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class InstaPic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pic",
                table: "InstaAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pic",
                table: "InstaAccounts");
        }
    }
}

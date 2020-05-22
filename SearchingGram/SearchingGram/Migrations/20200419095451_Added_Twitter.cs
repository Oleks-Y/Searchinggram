using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class Added_Twitter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_growsFollowers",
                table: "TwitterAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_growsRetweets",
                table: "TwitterAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_growsFollowers",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "_growsRetweets",
                table: "TwitterAccounts");
        }
    }
}

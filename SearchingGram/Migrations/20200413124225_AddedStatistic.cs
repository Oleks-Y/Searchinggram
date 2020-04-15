using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class AddedStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_growsFollowers",
                table: "TikTokAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_growsLikes",
                table: "TikTokAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_growsComments",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_growsFollowers",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_growsLikes",
                table: "InstaAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_growsFollowers",
                table: "TikTokAccounts");

            migrationBuilder.DropColumn(
                name: "_growsLikes",
                table: "TikTokAccounts");

            migrationBuilder.DropColumn(
                name: "_growsComments",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "_growsFollowers",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "_growsLikes",
                table: "InstaAccounts");
        }
    }
}

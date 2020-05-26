using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class TryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_error",
                table: "InstaAccounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_error",
                table: "InstaAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

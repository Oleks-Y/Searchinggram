using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class NewThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_Watchers_WatchOwnerId",
                table: "Monitors");

            migrationBuilder.DropIndex(
                name: "IX_Monitors_WatchOwnerId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "WatchOwnerId",
                table: "Monitors");

            migrationBuilder.AddColumn<int>(
                name: "WatcherId",
                table: "Monitors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_WatcherId",
                table: "Monitors",
                column: "WatcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_Watchers_WatcherId",
                table: "Monitors",
                column: "WatcherId",
                principalTable: "Watchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitors_Watchers_WatcherId",
                table: "Monitors");

            migrationBuilder.DropIndex(
                name: "IX_Monitors_WatcherId",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "WatcherId",
                table: "Monitors");

            migrationBuilder.AddColumn<int>(
                name: "WatchOwnerId",
                table: "Monitors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_WatchOwnerId",
                table: "Monitors",
                column: "WatchOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitors_Watchers_WatchOwnerId",
                table: "Monitors",
                column: "WatchOwnerId",
                principalTable: "Watchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

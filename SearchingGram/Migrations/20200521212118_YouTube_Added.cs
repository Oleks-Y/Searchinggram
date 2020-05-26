using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchingGram.Migrations
{
    public partial class YouTube_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowerCount",
                table: "TwitterAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxRetweets",
                table: "TwitterAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaxRetweets_Text",
                table: "TwitterAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinRetweets",
                table: "TwitterAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MinRetweets_Text",
                table: "TwitterAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetweetsCount",
                table: "TwitterAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ScreenName",
                table: "TwitterAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_retweetsList",
                table: "TwitterAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Business_category_name",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Follow",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Followers",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Full_name",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Is_business_account",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_error",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Max_comments",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Max_comments_pic",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Max_likes",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Max_likes_pic",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Min_comments",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Min_comments_pic",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Min_likes",
                table: "InstaAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Min_likes_pic",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_commentsList",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_likesList",
                table: "InstaAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "YouTubeAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MonitorOwnerId = table.Column<int>(nullable: false),
                    ChanelId = table.Column<string>(nullable: true),
                    Subscribers = table.Column<string>(nullable: true),
                    Views = table.Column<string>(nullable: true),
                    _viewsList = table.Column<string>(nullable: true),
                    _likes = table.Column<string>(nullable: true),
                    _dislikes = table.Column<string>(nullable: true),
                    _mostLiked = table.Column<long>(nullable: false),
                    _mostDisliked = table.Column<long>(nullable: false),
                    _commentsCounts = table.Column<string>(nullable: true),
                    VideosCount = table.Column<string>(nullable: true),
                    _videoNames = table.Column<string>(nullable: true),
                    _viewsGrows = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YouTubeAccounts_Monitors_MonitorOwnerId",
                        column: x => x.MonitorOwnerId,
                        principalTable: "Monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YouTubeAccounts_MonitorOwnerId",
                table: "YouTubeAccounts",
                column: "MonitorOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YouTubeAccounts");

            migrationBuilder.DropColumn(
                name: "FollowerCount",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "MaxRetweets",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "MaxRetweets_Text",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "MinRetweets",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "MinRetweets_Text",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "RetweetsCount",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "ScreenName",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "_retweetsList",
                table: "TwitterAccounts");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Business_category_name",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Follow",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Followers",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Full_name",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Is_business_account",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Is_error",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Max_comments",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Max_comments_pic",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Max_likes",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Max_likes_pic",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Min_comments",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Min_comments_pic",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Min_likes",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "Min_likes_pic",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "_commentsList",
                table: "InstaAccounts");

            migrationBuilder.DropColumn(
                name: "_likesList",
                table: "InstaAccounts");
        }
    }
}

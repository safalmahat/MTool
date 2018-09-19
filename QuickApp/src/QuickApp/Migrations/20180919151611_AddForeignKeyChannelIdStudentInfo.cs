using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickApp.Migrations
{
    public partial class AddForeignKeyChannelIdStudentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                    name: "FK_StudentRegistrationInfo_Channel_ChannelId",
                    table: "StudentRegistrationInfo",
                    column: "ChannelId",
                    principalTable: "Channel",
                    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

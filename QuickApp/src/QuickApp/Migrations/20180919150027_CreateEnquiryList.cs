using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace QuickApp.Migrations
{
    public partial class CreateEnquiryList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChannelName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                 name: "EnquiryList",
                 columns: table => new
                 {
                     CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                     UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                     UpdatedDate = table.Column<DateTime>(nullable: false),
                     CreatedDate = table.Column<DateTime>(nullable: false),
                     Id = table.Column<int>(nullable: false)
                         .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                     StudentId = table.Column<int>(nullable: false),
                     ChannelId = table.Column<int>(nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_EnquiryList", x => x.Id);
                 });

            migrationBuilder.AddColumn<int>(
                     name: "ChannelId ",
                     table: "StudentRegistrationInfo",
                     schema: "dbo",
                     nullable: false,
                     defaultValue: 1);

           


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "ChannelId",
               table: "dbo.StudentRegistrationInfo");
        }
    }
}

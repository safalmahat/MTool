using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickApp.Migrations
{
    public partial class UpdateStudentRegistrationInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                        name: "ColumnId",
                        table: "dbo.StudentRegistrationInfo",
                        nullable: false,
                        defaultValue: 1);

            migrationBuilder.AddForeignKey(
                     name: "FK_StudentRegistrationInfo_EnquiryList_ColumnId",
                     table: "StudentRegistrationInfo",
                     column: "ColumnId",
                     principalTable: "EnquiryList",
                     principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(

                name: "ColumnId",
                 table: "dbo.StudentRegistrationInfo");
        }
    }
}

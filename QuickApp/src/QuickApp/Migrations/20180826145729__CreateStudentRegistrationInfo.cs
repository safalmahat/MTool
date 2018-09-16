using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace QuickApp.Migrations
{
    public partial class _CreateStudentRegistrationInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "StudentRegistrationInfo",
                 columns: table => new
                 {
                     CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                     UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                     UpdatedDate = table.Column<DateTime>(nullable: false),
                     CreatedDate = table.Column<DateTime>(nullable: false),
                     Id = table.Column<int>(nullable: false)
                         .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                     Token = table.Column<string>(maxLength: 1000, nullable: false),
                     FirstName = table.Column<string>(maxLength: 100, nullable: false),
                     MiddleName = table.Column<string>(maxLength: 100, nullable: false),
                     LastName = table.Column<string>(maxLength: 100, nullable: false),
                     Email = table.Column<string>(maxLength: 100, nullable: true),
                     PhoneNumber = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                     Address = table.Column<string>(nullable: true),
                     City = table.Column<string>(maxLength: 50, nullable: true),
                     Gender = table.Column<int>(nullable: false),
                     CompletedLevel= table.Column<string>(maxLength: 100, nullable: true),
                     CompletedFaculty = table.Column<string>(maxLength: 50, nullable: true),
                     IntrestedFaculty = table.Column<string>(maxLength: 50, nullable: true),
                     Percentage = table.Column<string>(maxLength: 50, nullable: true),
                     DateCreated = table.Column<DateTime>(nullable: false),
                     DateModified = table.Column<DateTime>(nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_StudentRegistrationInfo", x => x.Id);
                 });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Obras.Data.Migrations
{
    public partial class AddResponsibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responsibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RegistrationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsibilities_AspNetUsers_ChangeUserId",
                        column: x => x.ChangeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responsibilities_AspNetUsers_RegistrationUserId",
                        column: x => x.RegistrationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responsibilities_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_ChangeUserId",
                table: "Responsibilities",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_CompanyId",
                table: "Responsibilities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibilities_RegistrationUserId",
                table: "Responsibilities",
                column: "RegistrationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsibilities");
        }
    }
}

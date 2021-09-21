using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Obras.Data.Migrations
{
    public partial class AddPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePeople = table.Column<int>(type: "int", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    CorporateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FantasyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Constructor = table.Column<bool>(type: "bit", nullable: false),
                    Investor = table.Column<bool>(type: "bit", nullable: false),
                    Client = table.Column<bool>(type: "bit", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Neighbourhood = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Complement = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RegistrationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peoples_AspNetUsers_ChangeUserId",
                        column: x => x.ChangeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Peoples_AspNetUsers_RegistrationUserId",
                        column: x => x.RegistrationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Peoples_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_ChangeUserId",
                table: "Peoples",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_CompanyId",
                table: "Peoples",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_RegistrationUserId",
                table: "Peoples",
                column: "RegistrationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peoples");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Obras.Data.Migrations
{
    public partial class AddOutsoursed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Outsourseds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypePeople = table.Column<int>(type: "int", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    CorporateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FantasyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    ResponsibilityId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RegistrationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outsourseds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Outsourseds_ChangeUserId",
                table: "Outsourseds",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Outsourseds_CompanyId",
                table: "Outsourseds",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Outsourseds_RegistrationUserId",
                table: "Outsourseds",
                column: "RegistrationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Outsourseds_ResponsibilityId",
                table: "Outsourseds",
                column: "ResponsibilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outsourseds");
        }
    }
}

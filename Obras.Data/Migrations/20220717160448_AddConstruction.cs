using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Obras.Data.Migrations
{
    public partial class AddConstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Constructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusConstruction = table.Column<int>(type: "int", nullable: false),
                    DateBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Neighbourhood = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Complement = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BatchArea = table.Column<int>(type: "int", nullable: true),
                    BuildingArea = table.Column<int>(type: "int", nullable: true),
                    MunicipalRegistration = table.Column<int>(type: "int", nullable: true),
                    License = table.Column<int>(type: "int", nullable: true),
                    UndergroundUse = table.Column<int>(type: "int", nullable: true),
                    Art = table.Column<int>(type: "int", nullable: true),
                    Cno = table.Column<int>(type: "int", nullable: true),
                    MotherEnrollment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    SaleValue = table.Column<double>(type: "float", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RegistrationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructions_AspNetUsers_ChangeUserId",
                        column: x => x.ChangeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_AspNetUsers_RegistrationUserId",
                        column: x => x.RegistrationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypePhoto = table.Column<int>(type: "int", nullable: false),
                    ConstructionId = table.Column<int>(type: "int", nullable: true),
                    ConstrucationId = table.Column<int>(type: "int", nullable: false),
                    RegistrationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_ChangeUserId",
                        column: x => x.ChangeUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_RegistrationUserId",
                        column: x => x.RegistrationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Constructions_ConstructionId",
                        column: x => x.ConstructionId,
                        principalTable: "Constructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ChangeUserId",
                table: "Constructions",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_CompanyId",
                table: "Constructions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_RegistrationUserId",
                table: "Constructions",
                column: "RegistrationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ChangeUserId",
                table: "Photos",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ConstructionId",
                table: "Photos",
                column: "ConstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RegistrationUserId",
                table: "Photos",
                column: "RegistrationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Constructions");
        }
    }
}

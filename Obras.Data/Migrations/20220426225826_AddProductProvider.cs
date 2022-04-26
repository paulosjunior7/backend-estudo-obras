using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Obras.Data.Migrations
{
    public partial class AddProductProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuxiliaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProviders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProviders_ChangeUserId",
                table: "ProductProviders",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProviders_ProductId",
                table: "ProductProviders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProviders_ProviderId",
                table: "ProductProviders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProviders_RegistrationUserId",
                table: "ProductProviders",
                column: "RegistrationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProviders");
        }
    }
}

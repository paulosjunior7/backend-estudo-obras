using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obras.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderFieldTypePeopleANDCPF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Providers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypePeople",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 74);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "TypePeople",
                table: "Providers");
        }
    }
}

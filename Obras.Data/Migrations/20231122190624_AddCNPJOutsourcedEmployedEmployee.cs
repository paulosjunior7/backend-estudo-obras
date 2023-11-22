using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obras.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCNPJOutsourcedEmployedEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Employees",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Employees",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Employed",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Outsourced",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TypePeople",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Employed",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Outsourced",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TypePeople",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Employees",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14,
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obras.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteObrigatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "Constructions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionManpowers_Employees_EmployeeId",
                table: "ConstructionManpowers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Constructions_ConstructionId",
                table: "ConstructionMaterials",
                column: "ConstructionId",
                principalTable: "Constructions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Groups_GroupId",
                table: "ConstructionMaterials",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Products_ProductId",
                table: "ConstructionMaterials",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Unities_UnityId",
                table: "ConstructionMaterials",
                column: "UnityId",
                principalTable: "Unities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionManpowers_Employees_EmployeeId",
                table: "ConstructionManpowers");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionManpowers_Outsourseds_OutsourcedId",
                table: "ConstructionManpowers");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionMaterials_Brands_BrandId",
                table: "ConstructionMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionMaterials_Constructions_ConstructionId",
                table: "ConstructionMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionMaterials_Groups_GroupId",
                table: "ConstructionMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionMaterials_Products_ProductId",
                table: "ConstructionMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionMaterials_Providers_ProviderId",
                table: "ConstructionMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionMaterials_Unities_UnityId",
                table: "ConstructionMaterials");

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "Constructions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionManpowers_Employees_EmployeeId",
                table: "ConstructionManpowers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionManpowers_Outsourseds_OutsourcedId",
                table: "ConstructionManpowers",
                column: "OutsourcedId",
                principalTable: "Outsourseds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Brands_BrandId",
                table: "ConstructionMaterials",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Constructions_ConstructionId",
                table: "ConstructionMaterials",
                column: "ConstructionId",
                principalTable: "Constructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Groups_GroupId",
                table: "ConstructionMaterials",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Products_ProductId",
                table: "ConstructionMaterials",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Providers_ProviderId",
                table: "ConstructionMaterials",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionMaterials_Unities_UnityId",
                table: "ConstructionMaterials",
                column: "UnityId",
                principalTable: "Unities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

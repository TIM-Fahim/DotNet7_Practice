using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B2BCompany.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class daf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ComapanyConfigID",
                table: "ConfigValues",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ComapnyId",
                table: "ConfigValues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComapnyId",
                table: "ConfigValues");

            migrationBuilder.AlterColumn<string>(
                name: "ComapanyConfigID",
                table: "ConfigValues",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

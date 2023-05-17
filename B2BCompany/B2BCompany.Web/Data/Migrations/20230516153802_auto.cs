using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B2BCompany.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class auto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComapanyConfigID",
                table: "ConfigValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComapanyConfigID",
                table: "ConfigValues");
        }
    }
}

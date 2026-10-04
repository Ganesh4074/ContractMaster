using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMasters.Migrations
{
    /// <inheritdoc />
    public partial class CreatedbyEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmail",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByEmail",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

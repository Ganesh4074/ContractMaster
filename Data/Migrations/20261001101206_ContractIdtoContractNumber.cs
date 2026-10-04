using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMasters.Migrations
{
    /// <inheritdoc />
    public partial class ContractIdtoContractNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Contracts",
                newName: "ContractNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractNumber",
                table: "Contracts",
                newName: "ContractId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMasters.Migrations
{
    /// <inheritdoc />
    public partial class addblobstorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfBlobName",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfBlobName",
                table: "Contracts");
        }
    }
}

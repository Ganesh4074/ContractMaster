using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMasters.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Contracts",
                newName: "Status");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "Contracts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Contracts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ApprovedAt",
                table: "Approvals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "RejectedAt",
                table: "Approvals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Approvals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Approvals");

            migrationBuilder.DropColumn(
                name: "RejectedAt",
                table: "Approvals");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Approvals");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Contracts",
                newName: "status");
        }
    }
}

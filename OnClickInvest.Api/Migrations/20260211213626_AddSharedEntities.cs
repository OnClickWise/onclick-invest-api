using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnClickInvest.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSharedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "audit_logs",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "audit_logs");
        }
    }
}

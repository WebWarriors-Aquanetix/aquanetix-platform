using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWarriors.Aquanetix.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDevicesAndAllBCs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "p_k_alerts",
                table: "alerts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alerts",
                table: "alerts",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_alerts",
                table: "alerts");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_alerts",
                table: "alerts",
                column: "id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWarriors.Aquanetix.Platform.Migrations
{
    /// <inheritdoc />
    public partial class SyncSchemaForGetAllAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_threshold_configurations_devices_DeviceId",
                table: "threshold_configurations");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "threshold_configurations",
                newName: "device_id_fk");

            migrationBuilder.RenameIndex(
                name: "IX_threshold_configurations_DeviceId",
                table: "threshold_configurations",
                newName: "IX_threshold_configurations_device_id_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_threshold_device",
                table: "threshold_configurations",
                column: "device_id_fk",
                principalTable: "devices",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_threshold_device",
                table: "threshold_configurations");

            migrationBuilder.RenameColumn(
                name: "device_id_fk",
                table: "threshold_configurations",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_threshold_configurations_device_id_fk",
                table: "threshold_configurations",
                newName: "IX_threshold_configurations_DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_threshold_configurations_devices_DeviceId",
                table: "threshold_configurations",
                column: "DeviceId",
                principalTable: "devices",
                principalColumn: "id");
        }
    }
}

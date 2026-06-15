using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebWarriors.Aquanetix.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceIdToAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    device_name = table.Column<string>(type: "longtext", nullable: false),
                    location = table.Column<string>(type: "longtext", nullable: false),
                    type = table.Column<string>(type: "longtext", nullable: false),
                    severity = table.Column<string>(type: "longtext", nullable: false),
                    message = table.Column<string>(type: "longtext", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    value = table.Column<double>(type: "double", nullable: false),
                    threshold = table.Column<double>(type: "double", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerts");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetApi.Migrations
{
    public partial class DepartureTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Departure",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departure",
                table: "Trips");
        }
    }
}

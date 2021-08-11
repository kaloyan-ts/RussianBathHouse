using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RussianBathHouse.Migrations
{
    public partial class reservationsChangedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedFrom",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReservedUntill",
                table: "Reservations",
                newName: "ReservedDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReservedHours",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedHours",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReservedDate",
                table: "Reservations",
                newName: "ReservedUntill");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedFrom",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

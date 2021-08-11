using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RussianBathHouse.Migrations
{
    public partial class RemovedTimeSpanColumnFromReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedHours",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ReservedDate",
                table: "Reservations",
                newName: "ReservedFrom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservedFrom",
                table: "Reservations",
                newName: "ReservedDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ReservedHours",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}

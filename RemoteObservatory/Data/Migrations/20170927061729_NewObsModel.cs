using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteObservatory.Data.Migrations
{
    public partial class NewObsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaptureMethod",
                table: "ObservationModel");

            migrationBuilder.DropColumn(
                name: "CoordinateSystem",
                table: "ObservationModel");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ObservationModel");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "ObservationModel");

            migrationBuilder.RenameColumn(
                name: "OrderingUser",
                table: "ObservationModel",
                newName: "OwnerID");

            migrationBuilder.RenameColumn(
                name: "OrderingUser",
                table: "FileModel",
                newName: "OwnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "ObservationModel",
                newName: "OrderingUser");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "FileModel",
                newName: "OrderingUser");

            migrationBuilder.AddColumn<int>(
                name: "CaptureMethod",
                table: "ObservationModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoordinateSystem",
                table: "ObservationModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "ObservationModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longtitude",
                table: "ObservationModel",
                nullable: true);
        }
    }
}

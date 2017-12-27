using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteObservatory.Data.Migrations
{
    public partial class AddImageFile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensetivitMethod",
                table: "FileModel");

            migrationBuilder.AddColumn<int>(
                name: "SensetivityMethod",
                table: "FileModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensetivityMethod",
                table: "FileModel");

            migrationBuilder.AddColumn<int>(
                name: "SensetivitMethod",
                table: "FileModel",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteObservatory.Data.Migrations
{
    public partial class AddObsCtrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObservationModel_AspNetUsers_OrderingUserId",
                table: "ObservationModel");

            migrationBuilder.DropIndex(
                name: "IX_ObservationModel_OrderingUserId",
                table: "ObservationModel");

            migrationBuilder.RenameColumn(
                name: "OrderingUserId",
                table: "ObservationModel",
                newName: "OrderingUser");

            migrationBuilder.AlterColumn<string>(
                name: "OrderingUser",
                table: "ObservationModel",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderingUser",
                table: "FileModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderingUser",
                table: "FileModel");

            migrationBuilder.RenameColumn(
                name: "OrderingUser",
                table: "ObservationModel",
                newName: "OrderingUserId");

            migrationBuilder.AlterColumn<string>(
                name: "OrderingUserId",
                table: "ObservationModel",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObservationModel_OrderingUserId",
                table: "ObservationModel",
                column: "OrderingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObservationModel_AspNetUsers_OrderingUserId",
                table: "ObservationModel",
                column: "OrderingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RemoteObservatory.Data.Migrations
{
    public partial class AddImageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "ObservationModel",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaptureMethod = table.Column<int>(nullable: false),
                    CoordinateSystem = table.Column<int>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longtitude = table.Column<string>(nullable: true),
                    ObjectID = table.Column<long>(nullable: false),
                    ObjectName = table.Column<string>(nullable: true),
                    ObservationStart = table.Column<DateTime>(nullable: false),
                    OrderingUserId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ObservationModel_AspNetUsers_OrderingUserId",
                        column: x => x.OrderingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorMethod = table.Column<int>(nullable: false),
                    ExposureTime = table.Column<int>(nullable: false),
                    FrameRate = table.Column<float>(nullable: false),
                    HorizontalOffset = table.Column<int>(nullable: false),
                    HorizontalResolution = table.Column<int>(nullable: false),
                    ObservationModelID = table.Column<long>(nullable: true),
                    SensetivitMethod = table.Column<int>(nullable: false),
                    SensetivityValue = table.Column<int>(nullable: false),
                    VerticalOffset = table.Column<int>(nullable: false),
                    VerticalResolution = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FileModel_ObservationModel_ObservationModelID",
                        column: x => x.ObservationModelID,
                        principalTable: "ObservationModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_ObservationModelID",
                table: "FileModel",
                column: "ObservationModelID");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationModel_OrderingUserId",
                table: "ObservationModel",
                column: "OrderingUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileModel");

            migrationBuilder.DropTable(
                name: "ObservationModel");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}

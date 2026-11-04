using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffReleaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class addnewtabledepartmentuserassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentUserAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    CanViewClearance = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveClearance = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AssignedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUserAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_DepartmentUserAssignments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentUserAssignments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserAssignments_DepartmentId",
                table: "DepartmentUserAssignments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentUserAssignments_UserId",
                table: "DepartmentUserAssignments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentUserAssignments");
        }
    }
}

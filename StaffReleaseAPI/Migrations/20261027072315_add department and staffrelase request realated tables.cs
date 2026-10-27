using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffReleaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class adddepartmentandstaffrelaserequestrealatedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "StaffReleaseRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastWorkingDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasErpIds = table.Column<bool>(type: "bit", nullable: false),
                    CashierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoldId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IVisionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancelledById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffReleaseRequests", x => x.RequestId);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentManagerAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ManagerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentManagerAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_DepartmentManagerAssignments_AspNetUsers_ManagerUserId",
                        column: x => x.ManagerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentManagerAssignments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRequiredDepartments",
                columns: table => new
                {
                    RequestRequiredDepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    AssignedManagerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApprovedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRequiredDepartments", x => x.RequestRequiredDepartmentId);
                    table.ForeignKey(
                        name: "FK_RequestRequiredDepartments_AspNetUsers_AssignedManagerUserId",
                        column: x => x.AssignedManagerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestRequiredDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestRequiredDepartments_StaffReleaseRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "StaffReleaseRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentManagerAssignments_DepartmentId",
                table: "DepartmentManagerAssignments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentManagerAssignments_ManagerUserId",
                table: "DepartmentManagerAssignments",
                column: "ManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRequiredDepartments_AssignedManagerUserId",
                table: "RequestRequiredDepartments",
                column: "AssignedManagerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRequiredDepartments_DepartmentId",
                table: "RequestRequiredDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRequiredDepartments_RequestId",
                table: "RequestRequiredDepartments",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentManagerAssignments");

            migrationBuilder.DropTable(
                name: "RequestRequiredDepartments");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "StaffReleaseRequests");
        }
    }
}

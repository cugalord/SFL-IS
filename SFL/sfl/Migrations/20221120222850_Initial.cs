using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sfl.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(145)", maxLength: 145, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParcelStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StaffRole",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    StreetName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    CityCode = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => new { x.StreetName, x.StreetNumber, x.CityCode });
                    table.ForeignKey(
                        name: "FK_Street_City_CityCode",
                        column: x => x.CityCode,
                        principalTable: "City",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    CityCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branch_Street_StreetName_StreetNumber_CityCode",
                        columns: x => new { x.StreetName, x.StreetNumber, x.CityCode },
                        principalTable: "Street",
                        principalColumns: new[] { "StreetName", "StreetNumber", "CityCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcel",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    ParcelStatusID = table.Column<int>(type: "int", nullable: false),
                    RecipientCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    RecipientStreetName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    RecipientStreetNumber = table.Column<int>(type: "int", nullable: false),
                    SenderCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SenderStreetName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    SenderStreetNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parcel_ParcelStatus_ParcelStatusID",
                        column: x => x.ParcelStatusID,
                        principalTable: "ParcelStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcel_Street_RecipientStreetName_RecipientStreetNumber_RecipientCode",
                        columns: x => new { x.RecipientStreetName, x.RecipientStreetNumber, x.RecipientCode },
                        principalTable: "Street",
                        principalColumns: new[] { "StreetName", "StreetNumber", "CityCode" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcel_Street_SenderStreetName_SenderStreetNumber_SenderCode",
                        columns: x => new { x.SenderStreetName, x.SenderStreetNumber, x.SenderCode },
                        principalTable: "Street",
                        principalColumns: new[] { "StreetName", "StreetNumber", "CityCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Staff_Branch_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_StaffRole_RoleID",
                        column: x => x.RoleID,
                        principalTable: "StaffRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobStatusID = table.Column<int>(type: "int", nullable: false),
                    JobTypeID = table.Column<int>(type: "int", nullable: false),
                    StaffUsername = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Job_JobStatus_JobStatusID",
                        column: x => x.JobStatusID,
                        principalTable: "JobStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_JobType_JobTypeID",
                        column: x => x.JobTypeID,
                        principalTable: "JobType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Staff_StaffUsername",
                        column: x => x.StaffUsername,
                        principalTable: "Staff",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobParcel",
                columns: table => new
                {
                    JobsID = table.Column<int>(type: "int", nullable: false),
                    ParcelsID = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobParcel", x => new { x.JobsID, x.ParcelsID });
                    table.ForeignKey(
                        name: "FK_JobParcel_Job_JobsID",
                        column: x => x.JobsID,
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobParcel_Parcel_ParcelsID",
                        column: x => x.ParcelsID,
                        principalTable: "Parcel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_StreetName_StreetNumber_CityCode",
                table: "Branch",
                columns: new[] { "StreetName", "StreetNumber", "CityCode" },
                unique: true,
                filter: "[CityCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobStatusID",
                table: "Job",
                column: "JobStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobTypeID",
                table: "Job",
                column: "JobTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_StaffUsername",
                table: "Job",
                column: "StaffUsername");

            migrationBuilder.CreateIndex(
                name: "IX_JobParcel_ParcelsID",
                table: "JobParcel",
                column: "ParcelsID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_ParcelStatusID",
                table: "Parcel",
                column: "ParcelStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_RecipientStreetName_RecipientStreetNumber_RecipientCode",
                table: "Parcel",
                columns: new[] { "RecipientStreetName", "RecipientStreetNumber", "RecipientCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_SenderStreetName_SenderStreetNumber_SenderCode",
                table: "Parcel",
                columns: new[] { "SenderStreetName", "SenderStreetNumber", "SenderCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_BranchID",
                table: "Staff",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RoleID",
                table: "Staff",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Street_CityCode",
                table: "Street",
                column: "CityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobParcel");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Parcel");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "ParcelStatus");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "StaffRole");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}

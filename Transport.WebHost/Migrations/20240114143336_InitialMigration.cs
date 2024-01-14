using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.WebHost.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Users_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RegisteredInCountry = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    Seats = table.Column<short>(type: "smallint", nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Vehicles_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Content = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserReviews_PK", x => x.Id);
                    table.ForeignKey(
                        name: "UserReviews_Users_FK",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailabilityStatus = table.Column<short>(type: "smallint", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Drivers_PK", x => x.Id);
                    table.ForeignKey(
                        name: "Drivers_Users_FK",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Drivers_Vehicles_FK",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    RequestedOn = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TookOffOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Rides_PK", x => x.Id);
                    table.ForeignKey(
                        name: "Rides_Drivers_FK",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Rides_UserReviews_FK",
                        column: x => x.UserReviewId,
                        principalTable: "UserReviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Rides_Users_FK",
                        column: x => x.RiderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReviewsDrivers",
                columns: table => new
                {
                    UserReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserReviewsDrivers_PK", x => new { x.UserReviewId, x.DriverId });
                    table.ForeignKey(
                        name: "UserReviewsDrivers_Drivers_FK",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "UserReviewsDrivers_UserReviews_FK",
                        column: x => x.UserReviewId,
                        principalTable: "UserReviews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RideStops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RideId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressText = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    OrderingNumber = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RideStops_PK", x => x.Id);
                    table.ForeignKey(
                        name: "RideStops_Rides_FK",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_VehicleId",
                table: "Drivers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DriverId",
                table: "Rides",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_RiderId",
                table: "Rides",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_UserReviewId",
                table: "Rides",
                column: "UserReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_RideStops_RideId",
                table: "RideStops",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReviews_AuthorId",
                table: "UserReviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReviewsDrivers_DriverId",
                table: "UserReviewsDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "UserReviewsDrivers_UN",
                table: "UserReviewsDrivers",
                column: "UserReviewId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideStops");

            migrationBuilder.DropTable(
                name: "UserReviewsDrivers");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "UserReviews");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubLogBook.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AircraftLogBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftLogBookId = table.Column<int>(nullable: false),
                    TaiNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftLogBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirCraftModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingDeviceKind = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    TypeDesignation = table.Column<string>(nullable: true),
                    ModelFullName = table.Column<string>(nullable: true),
                    LastAnnual = table.Column<DateTime>(nullable: true),
                    LastVOR = table.Column<DateTime>(nullable: true),
                    LastAltimeter = table.Column<DateTime>(nullable: true),
                    LastTransponder = table.Column<DateTime>(nullable: true),
                    LastELT = table.Column<DateTime>(nullable: true),
                    LastPitotStatic = table.Column<DateTime>(nullable: true),
                    NextAnnual = table.Column<DateTime>(nullable: true),
                    NextVOR = table.Column<DateTime>(nullable: true),
                    NextAltimeter = table.Column<DateTime>(nullable: true),
                    NextTransponder = table.Column<DateTime>(nullable: true),
                    NextELT = table.Column<DateTime>(nullable: true),
                    NextPitotStatic = table.Column<DateTime>(nullable: true),
                    Last100 = table.Column<double>(nullable: true),
                    OilChange = table.Column<DateTime>(nullable: true),
                    EngineTime = table.Column<double>(nullable: true),
                    RegistrationRenewalDate = table.Column<DateTime>(nullable: true),
                    FrequentlyUsed = table.Column<string>(nullable: true),
                    PublicNotes = table.Column<string>(nullable: true),
                    PrivateNotes = table.Column<string>(nullable: true),
                    FlightCount = table.Column<int>(nullable: false),
                    Hours = table.Column<double>(nullable: false),
                    FirstFlight = table.Column<DateTime>(nullable: true),
                    LastFlight = table.Column<DateTime>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCraftModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AircraftReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    IdNumber = table.Column<string>(nullable: true),
                    TailNumber = table.Column<string>(nullable: true),
                    ReservationInfo = table.Column<string>(nullable: true),
                    AircraftId = table.Column<int>(nullable: false),
                    PilotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomPropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    SortKey = table.Column<string>(nullable: true),
                    FormatString = table.Column<string>(nullable: true),
                    Type = table.Column<short>(nullable: false),
                    Flags = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomPropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailNumber = table.Column<string>(nullable: true),
                    Complex = table.Column<bool>(nullable: true),
                    HighPerformance = table.Column<bool>(nullable: true),
                    ConstantSpeedProp = table.Column<bool>(nullable: true),
                    TailWheel = table.Column<bool>(nullable: true),
                    Retractable = table.Column<bool>(nullable: true),
                    Turbine = table.Column<bool>(nullable: true),
                    Jet = table.Column<bool>(nullable: true),
                    Flaps = table.Column<bool>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    AircraftState = table.Column<int>(nullable: false),
                    AircraftCategory = table.Column<int>(nullable: false),
                    AircraftClass = table.Column<int>(nullable: false),
                    AirCraftModelId = table.Column<int>(nullable: false),
                    ClubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aircrafts_AirCraftModels_AirCraftModelId",
                        column: x => x.AirCraftModelId,
                        principalTable: "AirCraftModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aircrafts_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactBookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactBooks_ContactBookId",
                        column: x => x.ContactBookId,
                        principalTable: "ContactBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMAILs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMail = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAILs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMAILs_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    Height = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    ContactId = table.Column<int>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ClubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(nullable: true),
                    AreaCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checkrides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PilotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkrides_Members_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endorsments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GivenDate = table.Column<DateTime>(nullable: false),
                    ExpiredTime = table.Column<DateTime>(nullable: false),
                    PilotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endorsments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endorsments_Members_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    AircraftId = table.Column<int>(nullable: true),
                    PilotId = table.Column<int>(nullable: true),
                    Routh = table.Column<string>(nullable: true),
                    EngineStart = table.Column<decimal>(nullable: false),
                    EngineEnd = table.Column<decimal>(nullable: false),
                    HobbsStart = table.Column<decimal>(nullable: false),
                    HobbsEnd = table.Column<decimal>(nullable: false),
                    AircraftLogBookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_AircraftLogBooks_AircraftLogBookId",
                        column: x => x.AircraftLogBookId,
                        principalTable: "AircraftLogBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Members_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<int>(nullable: false),
                    PilotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_Members_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PilotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogBooks_Members_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    Routh = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    TotalTime = table.Column<decimal>(nullable: false),
                    PIC = table.Column<decimal>(nullable: false),
                    SIC = table.Column<decimal>(nullable: false),
                    Dual = table.Column<decimal>(nullable: false),
                    Solo = table.Column<decimal>(nullable: false),
                    CFI = table.Column<decimal>(nullable: false),
                    XCountry = table.Column<decimal>(nullable: false),
                    Night = table.Column<decimal>(nullable: false),
                    Instrument = table.Column<decimal>(nullable: false),
                    Simulated = table.Column<decimal>(nullable: false),
                    GroundSimulator = table.Column<decimal>(nullable: false),
                    Approaches = table.Column<short>(nullable: false),
                    Holds = table.Column<short>(nullable: false),
                    Landings = table.Column<short>(nullable: false),
                    NightLandings = table.Column<short>(nullable: false),
                    AerobaticsTime = table.Column<decimal>(nullable: false),
                    AgriculturalTime = table.Column<decimal>(nullable: false),
                    BannerTowingTime = table.Column<decimal>(nullable: false),
                    BushTime = table.Column<decimal>(nullable: false),
                    HobbsStart = table.Column<decimal>(nullable: false),
                    HobbsEnd = table.Column<decimal>(nullable: false),
                    EngineStart = table.Column<decimal>(nullable: false),
                    EngineEnd = table.Column<decimal>(nullable: false),
                    FlightComments = table.Column<string>(nullable: true),
                    AircraftId = table.Column<int>(nullable: true),
                    LogBookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRecords_Aircrafts_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightRecords_LogBooks_LogBookId",
                        column: x => x.LogBookId,
                        principalTable: "LogBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ContactId",
                table: "Addresses",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Aircrafts_AirCraftModelId",
                table: "Aircrafts",
                column: "AirCraftModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Aircrafts_ClubId",
                table: "Aircrafts",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkrides_PilotId",
                table: "Checkrides",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactBookId",
                table: "Contacts",
                column: "ContactBookId");

            migrationBuilder.CreateIndex(
                name: "IX_EMAILs_ContactId",
                table: "EMAILs",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Endorsments_PilotId",
                table: "Endorsments",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRecords_AircraftId",
                table: "FlightRecords",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRecords_LogBookId",
                table: "FlightRecords",
                column: "LogBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftId",
                table: "Flights",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftLogBookId",
                table: "Flights",
                column: "AircraftLogBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PilotId",
                table: "Flights",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_PilotId",
                table: "Licenses",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_LogBooks_PilotId",
                table: "LogBooks",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ClubId",
                table: "Members",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ContactId",
                table: "Members",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ContactId",
                table: "Phones",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AircraftReservations");

            migrationBuilder.DropTable(
                name: "Checkrides");

            migrationBuilder.DropTable(
                name: "CustomPropertyTypes");

            migrationBuilder.DropTable(
                name: "EMAILs");

            migrationBuilder.DropTable(
                name: "Endorsments");

            migrationBuilder.DropTable(
                name: "FlightRecords");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "LogBooks");

            migrationBuilder.DropTable(
                name: "Aircrafts");

            migrationBuilder.DropTable(
                name: "AircraftLogBooks");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "AirCraftModels");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactBooks");
        }
    }
}

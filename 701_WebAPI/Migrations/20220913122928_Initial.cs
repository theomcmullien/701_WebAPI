using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _701_WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RateOT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradeID = table.Column<int>(type: "int", nullable: true),
                    EstablishmentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "ChargeCode",
                columns: table => new
                {
                    ChargeCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeCode", x => x.ChargeCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Establishment",
                columns: table => new
                {
                    EstablishmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishment", x => x.EstablishmentID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPeriod",
                columns: table => new
                {
                    FinancialPeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weeks = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPeriod", x => x.FinancialPeriodID);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hours = table.Column<double>(type: "float", nullable: true),
                    HoursOT = table.Column<double>(type: "float", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkTypeID = table.Column<int>(type: "int", nullable: false),
                    ChargeCodeID = table.Column<int>(type: "int", nullable: false),
                    FinancialPeriodID = table.Column<int>(type: "int", nullable: true),
                    EstablishmentID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeID);
                });

            migrationBuilder.CreateTable(
                name: "WorkType",
                columns: table => new
                {
                    WorkTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkType", x => x.WorkTypeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ChargeCode");

            migrationBuilder.DropTable(
                name: "Establishment");

            migrationBuilder.DropTable(
                name: "FinancialPeriod");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "WorkType");
        }
    }
}

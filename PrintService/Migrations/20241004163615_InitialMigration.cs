using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__EFMigrationHistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFMigrationHistory", x => x.MigrationId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    PrinterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CampusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RoomNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Printers__D452AB21D9C48E45", x => x.PrinterID);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reports__D5BD48E54E9D1821", x => x.ReportID);
                });

            migrationBuilder.CreateTable(
                name: "SPSOConfigurations",
                columns: table => new
                {
                    ConfigID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultPagesPerSemester = table.Column<int>(type: "int", nullable: true),
                    DefaultPaperSize = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PermittedFileTypes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NextDefaultPagesDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateApply = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SPSOConf__C3BC333CA23CEA5E", x => x.ConfigID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountBalance = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__32C52A798CCF616C", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Account",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__1ABEEF6FF0AD0E35", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK__Documents__Stude__34C8D9D1",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseHistory",
                columns: table => new
                {
                    PurchaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PagesPurchased = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Purchase__6B0A6BDE2C71E164", x => x.PurchaseID);
                    table.ForeignKey(
                        name: "FK__PurchaseH__Stude__403A8C7D",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateTable(
                name: "PrintingLog",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PrinterID = table.Column<int>(type: "int", nullable: true),
                    DocumentID = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaperSize = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PagesPrinted = table.Column<int>(type: "int", nullable: true),
                    Copies = table.Column<int>(type: "int", nullable: true),
                    IsDoubleSided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Printing__5E5499A862BD064B", x => x.LogID);
                    table.ForeignKey(
                        name: "FK__PrintingL__Docum__3A81B327",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "DocumentID");
                    table.ForeignKey(
                        name: "FK__PrintingL__Print__398D8EEE",
                        column: x => x.PrinterID,
                        principalTable: "Printers",
                        principalColumn: "PrinterID");
                    table.ForeignKey(
                        name: "FK__PrintingL__Stude__38996AB5",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StudentID",
                table: "Documents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_PrintingLog_DocumentID",
                table: "PrintingLog",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_PrintingLog_PrinterID",
                table: "PrintingLog",
                column: "PrinterID");

            migrationBuilder.CreateIndex(
                name: "IX_PrintingLog_StudentID",
                table: "PrintingLog",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistory_StudentID",
                table: "PurchaseHistory",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students",
                table: "Students",
                column: "AccountID",
                unique: true,
                filter: "[AccountID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFMigrationHistory");

            migrationBuilder.DropTable(
                name: "PrintingLog");

            migrationBuilder.DropTable(
                name: "PurchaseHistory");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SPSOConfigurations");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Printers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}

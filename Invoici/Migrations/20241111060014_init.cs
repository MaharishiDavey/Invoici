using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Invoici.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Particular = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HSNCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "Date", "Description", "GSTNumber", "PartyName" },
                values: new object[,]
                {
                    { "1", new DateTime(2024, 11, 11, 11, 30, 13, 403, DateTimeKind.Local).AddTicks(3320), "Office Supplies Purchase", "27AABCU9603R1ZV", "ABC Corporation" },
                    { "2", new DateTime(2024, 11, 11, 11, 30, 13, 403, DateTimeKind.Local).AddTicks(3323), "IT Services Billing", "27AAECS1223M1ZV", "XYZ Industries" },
                    { "3", new DateTime(2024, 11, 11, 11, 30, 13, 403, DateTimeKind.Local).AddTicks(3325), "Raw Materials Invoice", "27AAGCS1223R1ZV", "LMN Traders" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "InvoiceItemId", "HSNCode", "InvoiceId", "Particular", "Quantity", "Rate" },
                values: new object[,]
                {
                    { 1, "1234", "1", "Product A", 5, 100m },
                    { 2, "5678", "1", "Product B", 3, 200m },
                    { 3, "9012", "2", "Service C", 1, 500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}

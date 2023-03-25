using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderTable",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTable", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "WindowTable",
                columns: table => new
                {
                    WindowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTableOrderId = table.Column<int>(type: "int", nullable: false),
                    WindowName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityOfWindows = table.Column<int>(type: "int", nullable: false),
                    TotalSubElements = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindowTable", x => x.WindowID);
                    table.ForeignKey(
                        name: "FK_WindowTable_OrderTable_OrderTableOrderId",
                        column: x => x.OrderTableOrderId,
                        principalTable: "OrderTable",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubElementTable",
                columns: table => new
                {
                    SubElementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WindowTableWindowID = table.Column<int>(type: "int", nullable: false),
                    Element = table.Column<int>(type: "int", nullable: false),
                    SubElementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubElementTable", x => x.SubElementID);
                    table.ForeignKey(
                        name: "FK_SubElementTable_WindowTable_WindowTableWindowID",
                        column: x => x.WindowTableWindowID,
                        principalTable: "WindowTable",
                        principalColumn: "WindowID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderTable",
                columns: new[] { "OrderId", "Name", "State" },
                values: new object[,]
                {
                    { 1, "New York Building 1", "NY" },
                    { 2, "California Hotel AJK", "CA" }
                });

            migrationBuilder.InsertData(
                table: "WindowTable",
                columns: new[] { "WindowID", "OrderTableOrderId", "QuantityOfWindows", "TotalSubElements", "WindowName" },
                values: new object[,]
                {
                    { 1, 1, 4, 3, "A51" },
                    { 2, 1, 2, 1, "C Zone 5" },
                    { 3, 2, 3, 2, "GLB" },
                    { 4, 2, 10, 2, "OHF" }
                });

            migrationBuilder.InsertData(
                table: "SubElementTable",
                columns: new[] { "SubElementID", "Element", "Height", "SubElementType", "Width", "WindowTableWindowID" },
                values: new object[,]
                {
                    { 1, 1, 1850, "Doors", 1200, 1 },
                    { 2, 2, 1850, "Window", 800, 1 },
                    { 3, 3, 1850, "Window", 700, 1 },
                    { 4, 1, 2000, "Window", 1500, 2 },
                    { 5, 1, 2200, "Doors", 1400, 3 },
                    { 6, 2, 2200, "Window", 600, 3 },
                    { 7, 1, 2000, "Window", 1500, 4 },
                    { 8, 2, 2000, "Window", 1500, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubElementTable_WindowTableWindowID",
                table: "SubElementTable",
                column: "WindowTableWindowID");

            migrationBuilder.CreateIndex(
                name: "IX_WindowTable_OrderTableOrderId",
                table: "WindowTable",
                column: "OrderTableOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubElementTable");

            migrationBuilder.DropTable(
                name: "WindowTable");

            migrationBuilder.DropTable(
                name: "OrderTable");
        }
    }
}

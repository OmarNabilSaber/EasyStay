using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyStay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addvillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Villas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    Villa_Number = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.Villa_Number);
                    table.ForeignKey(
                        name: "FK_VillaNumbers_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VillaNumbers",
                columns: new[] { "Villa_Number", "SpecialDetails", "VillaId" },
                values: new object[,]
                {
                    { 101, "This villa number 101 is a royal villa with all the premium facilities.", 1 },
                    { 102, "This villa number 102 is a premium pool villa with all the premium facilities.", 2 },
                    { 103, "This villa number 103 is a royal villa with all the premium facilities.", 3 },
                    { 106, "This villa number 104 is a premium pool villa with all the premium facilities.", 6 },
                    { 201, "This villa number 201 is a royal villa with all the premium facilities.", 1 },
                    { 202, "This villa number 202 is a premium pool villa with all the premium facilities.", 2 },
                    { 203, "This villa number 203 is a royal villa with all the premium facilities.", 3 },
                    { 301, "This villa number 301 is a royal villa with all the premium facilities.", 1 },
                    { 302, "This villa number 302 is a premium pool villa with all the premium facilities.", 2 },
                    { 303, "This villa number 303 is a royal villa with all the premium facilities.", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

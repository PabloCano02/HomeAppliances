using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAppliances.Migrations
{
    /// <inheritdoc />
    public partial class HomeApplianceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeAppliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeApplianceTypeId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeAppliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeAppliances_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAppliances_HomeApplianceTypes_HomeApplianceTypeId",
                        column: x => x.HomeApplianceTypeId,
                        principalTable: "HomeApplianceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeAppliances_BrandId",
                table: "HomeAppliances",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAppliances_HomeApplianceTypeId",
                table: "HomeAppliances",
                column: "HomeApplianceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeAppliances");
        }
    }
}

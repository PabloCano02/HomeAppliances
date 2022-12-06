using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAppliances.Migrations
{
    /// <inheritdoc />
    public partial class HomeAppliancePhotoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeAppliancePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeApplianceId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeAppliancePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeAppliancePhotos_HomeAppliances_HomeApplianceId",
                        column: x => x.HomeApplianceId,
                        principalTable: "HomeAppliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeAppliancePhotos_HomeApplianceId",
                table: "HomeAppliancePhotos",
                column: "HomeApplianceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeAppliancePhotos");
        }
    }
}

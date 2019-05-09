using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace House.Data.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ShadeColor = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    ProductTypesId = table.Column<int>(nullable: false),
                    SpecialTagsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypeses_ProductTypesId",
                        column: x => x.ProductTypesId,
                        principalTable: "ProductTypeses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_SpecialTagses_SpecialTagsId",
                        column: x => x.SpecialTagsId,
                        principalTable: "SpecialTagses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypesId",
                table: "Products",
                column: "ProductTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SpecialTagsId",
                table: "Products",
                column: "SpecialTagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

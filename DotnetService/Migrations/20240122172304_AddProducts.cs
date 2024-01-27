using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheFlowerplace.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "A classic red rose, perfect for all occasions.", "images/rose.jpg", "Rose", 15.99m },
                    { 2, "Colorful tulips to brighten your day.", "images/tulip.jpg", "Tulip", 9.99m },
                    { 3, "Cheerful daisies with a fresh look.", "images/daisy.jpg", "Daisy", 5.99m },
                    { 4, "Bright and beautiful sunflowers.", "images/sunflower.jpg", "Sunflower", 12.50m },
                    { 5, "Elegant and exotic orchids.", "images/orchid.jpg", "Orchid", 22.99m },
                    { 6, "Stunning lilies for a beautiful bouquet.", "images/lily.jpg", "Lily", 18.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

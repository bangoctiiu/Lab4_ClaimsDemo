using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab4_ClaimsDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 201,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1602810318383-e72903fdda7a?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 202,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1539990121801-0471a6c5aa53?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 203,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 204,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1600185365316-76e98b3bc19d?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 101,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1618354691438-25e9c1c7c1b6?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 102,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1523381294911-8d3cead13475?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 103,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?w=800&q=80");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 104,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1582552956871-1d57c8dd63d1?w=800&q=80");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 201,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1583226353872-1e2f2e80dba6?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 202,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1606813907291-1e94e3a1a996?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 203,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1618354691438-25e9c1c7c1b6?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 204,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1600185365316-76e98b3bc19d?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 101,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 102,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1602810318383-e72903fdda7a?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 103,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1503341455253-b2e723bb3dbb?q=80&w=1000");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 104,
                column: "MainImage",
                value: "https://images.unsplash.com/photo-1539990121801-0471a6c5aa53?q=80&w=1000");
        }
    }
}

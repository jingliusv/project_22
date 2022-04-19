using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_22.Server.Migrations
{
    public partial class dataseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Kamera" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Kamera Väska" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ArticleNumber", "CategoryId", "Description", "ImageUrl", "Price", "ProductName" },
                values: new object[] { 1, "VBA420AE", 1, "Nikon D750 är en digital systemkamera som ger dig frihet att lyckas fånga alla motiv.", "https://images.unsplash.com/photo-1621520291095-aa6c7137f048?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80", 11900m, "Nikon D750" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ArticleNumber", "CategoryId", "Description", "ImageUrl", "Price", "ProductName" },
                values: new object[] { 2, "20945", 1, "Polaroid Now är en klassisk direktfilmskamera som plåtar och framkallar polaroidbilder med riktig retrokänsla. ", "https://www.kjell.com/globalassets/productimages/839191_20945.tif?ref=D5872CA259&format=jpg&w=960&h=960&mode=pad", 2100m, "Polaroid Now" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ArticleNumber", "CategoryId", "Description", "ImageUrl", "Price", "ProductName" },
                values: new object[] { 3, "VINTA145", 2, "This bag is perfect for a lifestyle of adventure, trekking to and from the studio, the office or the mountains.", "https://images.unsplash.com/photo-1547949003-9792a18a2601?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80", 1800m, "VINTA TYPE-II" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_22.Server.Migrations
{
    public partial class addnewdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Hörlurar" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Högtalare" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ArticleNumber", "CategoryId", "Description", "ImageUrl", "Price", "ProductName" },
                values: new object[] { 4, "1000XM4", 3, "WH-1000XM4-hörlurarna kombinerar elegant design och komfort på hög nivå. Supermjuka, tryckavlastande kuddar i uretanskum ger jämnt tryck och ökad kontakt med öronen för god passform. Den lägre vikten gör att du knappt kommer känna att du har dem på dig.", "https://images.unsplash.com/photo-1546435770-a3e426bf472b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1165&q=80", 1699m, "Sony WH-1000XM4" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ArticleNumber", "CategoryId", "Description", "ImageUrl", "Price", "ProductName" },
                values: new object[] { 5, "JBLGO3NHJ", 4, "JBL GO 3 är den perfekta högtalaren att ta med dig på språng! Med färgladd tyg och uttrycksfulla detaljer som är inspi- rerade av street style, är detta tillbehör en nödvändighet på din nästa utflykt. GO 3 är vatten- och dammtät enligt IP67 och har en integrerade ögla som gör att du kan bära med den vart som helst.", "https://images.unsplash.com/photo-1608223652565-63abae6cf733?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80", 365m, "JBL GO 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

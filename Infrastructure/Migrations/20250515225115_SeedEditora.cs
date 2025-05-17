using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCCAA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEditora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Editora",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Editora Abril" },
                    { 2, "Editora Moderna" },
                    { 3, "Editora Saraiva" },
                    { 4, "Editora Globo" },
                    { 5, "Editora Objetiva" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "Editora",
               keyColumn: "Id",
               keyValues: new object[] { 1, 2, 3, 4, 5 });
        }
    }
}

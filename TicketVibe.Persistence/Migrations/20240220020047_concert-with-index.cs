using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketVibe.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class concertwithindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Musicales");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genres",
                newSchema: "Musicales");

            migrationBuilder.CreateTable(
                name: "Concert",
                schema: "Musicales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DateEvent = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    TicketQuantity = table.Column<int>(type: "int", nullable: false),
                    Finalized = table.Column<bool>(type: "bit", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concert_Genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "Musicales",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concert_GenreId",
                schema: "Musicales",
                table: "Concert",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_Title",
                schema: "Musicales",
                table: "Concert",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concert",
                schema: "Musicales");

            migrationBuilder.RenameTable(
                name: "Genres",
                schema: "Musicales",
                newName: "Genres");
        }
    }
}

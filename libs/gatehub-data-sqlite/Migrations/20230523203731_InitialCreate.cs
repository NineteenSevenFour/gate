using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NineteenSevenFour.Gatehub.Data.sqlite.Migrations
{
  /// <inheritdoc />
  public partial class InitialCreate : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "applicationmetadata",
          columns: table => new
          {
            id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: false),
            description = table.Column<string>(type: "TEXT", nullable: false),
            icon = table.Column<string>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_applicationmetadata", x => x.id);
          });

      migrationBuilder.CreateIndex(
          name: "applicationmetadata_index",
          table: "applicationmetadata",
          column: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "applicationmetadata");
    }
  }
}

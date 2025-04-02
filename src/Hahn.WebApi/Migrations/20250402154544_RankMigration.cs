using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hahn.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RankMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CoinLoreId",
                table: "Cryptos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Cryptos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Cryptos");

            migrationBuilder.AlterColumn<int>(
                name: "CoinLoreId",
                table: "Cryptos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}

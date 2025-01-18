using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace privatext.Database.Migrations
{
    /// <inheritdoc />
    public partial class keyidentifer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyIdentifier",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyIdentifier",
                table: "Messages");
        }
    }
}

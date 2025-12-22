using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Programlama_Proje.Migrations
{
    /// <inheritdoc />
    public partial class SwitchToUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "PhoneNumber");
        }
    }
}

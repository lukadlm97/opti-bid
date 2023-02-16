using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiBid.Microservices.Accounts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFirstLogInField202302160158 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstLogIn",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstLogIn",
                table: "Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApi.Migrations
{
    /// <inheritdoc />
    public partial class updatedEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_MemberShips_MembershipId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MembershipId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_MembershipId",
                table: "Users",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_MemberShips_MembershipId",
                table: "Users",
                column: "MembershipId",
                principalTable: "MemberShips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

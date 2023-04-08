using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atividade2.Migrations
{
    /// <inheritdoc />
    public partial class TreinoPersonalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Treinos_PersonalID",
                table: "Treinos",
                column: "PersonalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Personals_PersonalID",
                table: "Treinos",
                column: "PersonalID",
                principalTable: "Personals",
                principalColumn: "PersonalID",
                onDelete: ReferentialAction.NoAction);//ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Personals_PersonalID",
                table: "Treinos");

            migrationBuilder.DropIndex(
                name: "IX_Treinos_PersonalID",
                table: "Treinos");
        }
    }
}

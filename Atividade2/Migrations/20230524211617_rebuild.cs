using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atividade2.Migrations
{
    /// <inheritdoc />
    public partial class rebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioTreino_Exercicios_ExerciciosExercicioID",
                table: "ExercicioTreino");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioTreino_Treinos_TreinosTreinoID",
                table: "ExercicioTreino");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Usuario_AlunoID",
                table: "Treinos");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Usuario_PersonalID",
                table: "Treinos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Usuario_Aluno_PersonalID",
                table: "Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioTreino_Exercicios_ExerciciosExercicioID",
                table: "ExercicioTreino",
                column: "ExerciciosExercicioID",
                principalTable: "Exercicios",
                principalColumn: "ExercicioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioTreino_Treinos_TreinosTreinoID",
                table: "ExercicioTreino",
                column: "TreinosTreinoID",
                principalTable: "Treinos",
                principalColumn: "TreinoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Usuario_AlunoID",
                table: "Treinos",
                column: "AlunoID",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Usuario_PersonalID",
                table: "Treinos",
                column: "PersonalID",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Usuario_Aluno_PersonalID",
                table: "Usuario",
                column: "Aluno_PersonalID",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioTreino_Exercicios_ExerciciosExercicioID",
                table: "ExercicioTreino");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioTreino_Treinos_TreinosTreinoID",
                table: "ExercicioTreino");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Usuario_AlunoID",
                table: "Treinos");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinos_Usuario_PersonalID",
                table: "Treinos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Usuario_Aluno_PersonalID",
                table: "Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioTreino_Exercicios_ExerciciosExercicioID",
                table: "ExercicioTreino",
                column: "ExerciciosExercicioID",
                principalTable: "Exercicios",
                principalColumn: "ExercicioID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioTreino_Treinos_TreinosTreinoID",
                table: "ExercicioTreino",
                column: "TreinosTreinoID",
                principalTable: "Treinos",
                principalColumn: "TreinoID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Usuario_AlunoID",
                table: "Treinos",
                column: "AlunoID",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinos_Usuario_PersonalID",
                table: "Treinos",
                column: "PersonalID",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Usuario_Aluno_PersonalID",
                table: "Usuario",
                column: "Aluno_PersonalID",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

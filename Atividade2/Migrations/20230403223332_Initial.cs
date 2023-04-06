using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atividade2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercicios",
                columns: table => new
                {
                    ExercicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicios", x => x.ExercicioID);
                });

            migrationBuilder.CreateTable(
                name: "Personals",
                columns: table => new
                {
                    PersonalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personals", x => x.PersonalID);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    AlunoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    E_Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalID = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.AlunoID);
                    table.ForeignKey(
                        name: "FK_Alunos_Personals_PersonalID",
                        column: x => x.PersonalID,
                        principalTable: "Personals",
                        principalColumn: "PersonalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treinos",
                columns: table => new
                {
                    TreinoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalID = table.Column<int>(type: "int", nullable: false),
                    AlunoID = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinos", x => x.TreinoID);
                    table.ForeignKey(
                        name: "FK_Treinos_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExercicioTreino",
                columns: table => new
                {
                    ExerciciosExercicioID = table.Column<int>(type: "int", nullable: false),
                    TreinosTreinoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercicioTreino", x => new { x.ExerciciosExercicioID, x.TreinosTreinoID });
                    table.ForeignKey(
                        name: "FK_ExercicioTreino_Exercicios_ExerciciosExercicioID",
                        column: x => x.ExerciciosExercicioID,
                        principalTable: "Exercicios",
                        principalColumn: "ExercicioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercicioTreino_Treinos_TreinosTreinoID",
                        column: x => x.TreinosTreinoID,
                        principalTable: "Treinos",
                        principalColumn: "TreinoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_PersonalID",
                table: "Alunos",
                column: "PersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_ExercicioTreino_TreinosTreinoID",
                table: "ExercicioTreino",
                column: "TreinosTreinoID");

            migrationBuilder.CreateIndex(
                name: "IX_Treinos_AlunoID",
                table: "Treinos",
                column: "AlunoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercicioTreino");

            migrationBuilder.DropTable(
                name: "Exercicios");

            migrationBuilder.DropTable(
                name: "Treinos");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Personals");
        }
    }
}

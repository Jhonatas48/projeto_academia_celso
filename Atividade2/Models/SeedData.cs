using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace Atividade2.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            //associa os dados ao contexto
            Context context = app.ApplicationServices.GetRequiredService<Context>();
            //inserir os dados nas entidades do contexto
            context.Database.Migrate();
            //se o contexto estiver vazio
            if (!context.Personals.Any())
            //inserir os dados iniciais
            {
                //context.Personals.AddRange(
                //    new Personal { Especialidade = "Esp 1", Nome = "Paulo Primeiro", PersonalID = 1 },
                //    new Personal { Especialidade = "Esp 2", Nome = "Saulo Segundo", PersonalID = 2 },
                //    new Personal { Especialidade = "Esp 3", Nome = "Tony Terceiro", PersonalID = 3 },
                //    new Personal { Especialidade = "Esp 4", Nome = "Quati Quarto", PersonalID = 4 });

                //context.Alunos.AddRange(
                //    new Aluno { Nome = "Cardius Maximus", Data_Nascimento = new DateTime(2000, 1, 1, 13, 0, 0), E_Mail = "cardius.max@gmail.com", Telefone = "99999-9999", Instagram = "@cardimax", Observacoes = "Gosta de cardio", PersonalID = 1, AlunoID = 1 },
                //    new Aluno { Nome = "Monty Python", Data_Nascimento = new DateTime(1987, 2, 2, 14, 0, 0), E_Mail = "monty.py@gmail.com", Telefone = "99323-5128", Instagram = "@montypy", Observacoes = "Não reconhece seus ferimentos", PersonalID = 2, AlunoID = 2 },
                //    new Aluno { Nome = "Square Squire", Data_Nascimento = new DateTime(1975, 3, 3, 15, 0, 0), E_Mail = "sqsq@gmail.com", Telefone = "98484-6262", Instagram = "@sq.sq", Observacoes = "Quadrado quadrado", PersonalID = 2, AlunoID = 3 }
                //    );

                //context.Exercicios.AddRange(
                //    new Exercicio { Nome = "Agachamentos", Categoria = "Resistência", Descricao = "sente no ar e levante", ExercicioID = 1 },
                //    new Exercicio { Nome = "Supino", Categoria = "Força", Descricao = "deitado, levante a barra com os pesos", ExercicioID = 2 },
                //    new Exercicio { Nome = "Peck-deck", Categoria = "Força", Descricao = "junte os braços segurando os pesos, e volte a posicao normal lentamente", ExercicioID = 3 },
                //    new Exercicio { Nome = "Flexões", Categoria = "Resistência", Descricao = "deitado de frente no solo, apoie as maos no chao na altura dos ombros e estique os bracos, se levanando e abaixando", ExercicioID = 4 },
                //    new Exercicio { Nome = "Abdominais", Categoria = "Resistência", Descricao = "deitado, mantenha suas pernas paradas e levante o tronco em 90 graus, voltando lentamente a posicao original", ExercicioID = 5 }
                //    );

                //context.Treinos.AddRange(
                //    new Treino { AlunoID = 1, PersonalID = 1, Data = new DateTime(2023, 1, 1), Hora = new DateTime(1, 1, 1, 15, 0, 0), TreinoID = 1 },
                //    new Treino { AlunoID = 1, PersonalID = 1, Data = new DateTime(2023, 1, 1), Hora = new DateTime(1, 1, 1, 15, 0, 0), TreinoID = 2 },
                //    new Treino { AlunoID = 1, PersonalID = 1, Data = new DateTime(2023, 1, 1), Hora = new DateTime(1, 1, 1, 15, 0, 0), TreinoID = 3 },
                //    new Treino { AlunoID = 1, PersonalID = 1, Data = new DateTime(2023, 1, 1), Hora = new DateTime(1, 1, 1, 15, 0, 0), TreinoID = 4 }
                //    );

                ////context.ExercicioTreino.AddRange();

                context.SaveChanges();
            }
        }
    }
}

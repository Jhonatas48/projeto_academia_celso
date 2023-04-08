using System.ComponentModel.DataAnnotations;

namespace Atividade2.Models
{
    public class Treino
    {
        public int TreinoID { get; set; }
        public int PersonalID { get; set; }
        public int AlunoID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }


        public Aluno Aluno { get; set; }
        public Personal Personal { get; set; }
        public ICollection<Exercicio> Exercicios { get; set; }
    }
}

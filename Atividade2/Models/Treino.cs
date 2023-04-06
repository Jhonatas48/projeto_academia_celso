namespace Atividade2.Models
{
    public class Treino
    {
        public int TreinoID { get; set; }
        public int PersonalID { get; set; }
        public int AlunoID { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public Aluno Aluno { get; set; }
        public IEnumerable<Exercicio> Exercicios { get; set; }
    }
}

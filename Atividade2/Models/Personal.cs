namespace Atividade2.Models
{
    public class Personal : Usuario
    {
        public int PersonalID { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }


        public ICollection<Aluno> Alunos { get; set; }
    }
}

﻿namespace Atividade2.Models
{
    public class Personal
    {
        public int PersonalID { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public IEnumerable<Aluno> Alunos { get; set; }
    }
}
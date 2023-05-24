using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Atividade2.Models
{
    abstract public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Forneça um endereço de email válido")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}

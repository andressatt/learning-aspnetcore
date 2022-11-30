using ControleDeContatos.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        [Key]
        public int UsuarioID { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string UsuarioNome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Senha { get; set; }

        public PerfilEnum Perfil { get; set; }

        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Data de Atualização")]
        public DateTime? DataAtualização { get; set; }
    }
}

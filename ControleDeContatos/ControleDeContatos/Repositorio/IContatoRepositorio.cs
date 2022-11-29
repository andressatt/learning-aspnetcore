using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        // Um contrato que vai receber como parametro um objeto contrato e que vai
        // retornar ele mesmo deve ser usado na classe ContatoRepositorio por injeção
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel ListarPorId(int Id);
    }
}

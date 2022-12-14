using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class ContatoController : Controller
    {
        // 2 - criamos esta variável para que ela carregue o contrato e faça
        //     o tratamento dentro desta classe por isso private e readonly.
        private readonly IContatoRepositorio _contatoRepositorio;

        // 1 - inserir uma injeção para o construtor do IContatoRepositorio.
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MsgSucesso"] = "Contato cadastrado com sucesso.";

                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Falha no cadastro. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MsgSucesso"] = "Contato atualizado com sucesso.";

                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Falha na alteração. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagou = _contatoRepositorio.Apagar(id);
                if (apagou)
                {
                    TempData["MsgSucesso"] = "Contato removido com sucesso.";
                }
                else
                {
                    TempData["MsgErro"] = $"Falha na remoção.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Falha na remoção. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

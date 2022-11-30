using ControleDeContatos.Enums;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeContatos.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            PopularViewBags();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario.DataCadastro = DateTime.Now;
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MsgSucesso"] = "Usuário cadastrado com sucesso.";

                    return RedirectToAction("Index");
                }
                PopularViewBags();
                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Falha no cadastro. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            PopularViewBags();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MsgSucesso"] = "Usuário atualizado com sucesso.";

                    return RedirectToAction("Index");
                }
                PopularViewBags();
                return View("Editar", usuario);
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = $"Falha na alteração. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel contato = _usuarioRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagou = _usuarioRepositorio.Apagar(id);
                if (apagou)
                {
                    TempData["MsgSucesso"] = "Usuário removido com sucesso.";
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

        private void PopularViewBags()
        {
            ViewBag.Perfis =
                new SelectList(
                    Enum.GetValues(typeof(PerfilEnum)).Cast<PerfilEnum>().ToDictionary(x => (int)x, x => x),
                    "Key", "Value"
                );
        }
    }
}

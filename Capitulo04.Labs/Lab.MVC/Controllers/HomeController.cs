using Lab.MVC.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Lab.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarErro()
        {
            ViewBag.MensagemErro = "Erro interno do servidor";
            return View("_Erro");
        }

        public ActionResult ListarSuppliers()
        {
            var db = new NorthwindEntities();
            return View(db.Suppliers.ToList());
        }

        public ActionResult ListarProdutos()
        {
            var db = new Model1Container();
            return View(db.Produtos.ToList());
        }

        public ActionResult ListarCategorias()
        {
            var db = new LojaContext("minhaConexao");
            return View(db.Categorias.ToList());
        }

        public ActionResult DetalharCategoria(int id)
        {
            var db = new LojaContext("minhaConexao");
            if (db.Categorias == null)
            {
                return new HttpNotFoundResult();
            }

            var categoria = db.Categorias.FirstOrDefault(x => x.CategoriaId == id);
            if (categoria == null)
            {
                return new HttpNotFoundResult();
            }
            return View(categoria);
        }

        public ActionResult IncluirCategoria()
        {
            string random = Path.GetRandomFileName().Replace(".", "");
            var db = new LojaContext("minhaConexao");
            var c = new Models.Categoria() { Descricao = $"Test {random}" };
            db.Categorias.Add(c);
            db.SaveChanges();

            return RedirectToAction(nameof(ListarCategorias));
        }
    }
}
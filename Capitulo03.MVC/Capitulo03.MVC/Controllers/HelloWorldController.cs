using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Capitulo03.MVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Welcome(string nome, int idade)
        {
            return HtmlEncoder.Default.Encode($"Nome: {nome}, idade: {idade}");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            //return "Controller HelloWorld - Método: Index";
            return View();
        }

        public IActionResult Welcome(string name, int numTimes)
        {
            //return HtmlEncoder.Default.Encode($"Seja bem-vindo: {name}! Nº vezes: {numTimes}.");
            
            ViewData["Msg"] = "Olá " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}

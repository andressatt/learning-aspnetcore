using Microsoft.AspNetCore.Mvc;

namespace Lab.Contas.Controllers
{
    public class ContaReceberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

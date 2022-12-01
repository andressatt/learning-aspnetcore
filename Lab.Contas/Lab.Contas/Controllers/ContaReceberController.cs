using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Contas.Controllers
{
    public class ContaReceberController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}

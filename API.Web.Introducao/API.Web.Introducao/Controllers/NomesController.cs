using Microsoft.AspNetCore.Mvc;

namespace API.Web.Introducao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NomesController : ControllerBase
    {
        [HttpGet]
        public string[] GetNomes()
        {
            string[] nomes = { "John", "Mary", "Kelly", "Sean" };

            return nomes;
        }
    }
}

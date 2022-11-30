using Microsoft.AspNetCore.Mvc;

namespace API.Web.Introducao.Controllers
{
    [ApiController]
    [Route("api/receitas")]
    public class ReceitasController : ControllerBase
    {
        [HttpGet]
        public string[] GetReceitas()
        {
            string[] bolos = { "Laranja", "Cenoura", "Chocolate", "Fubá" };

            return bolos;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteReceita(string id)
        {
            bool receitaRuim = false;

            return receitaRuim ? BadRequest() : NoContent();
        }
    }
}

using Lab.Contas.Data;
using Lab.Contas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.Contas.Controllers
{
    public class TipoContaPagarController : Controller
    {
        private readonly LabContasContext _context;

        public TipoContaPagarController(LabContasContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoContaPagar.ToListAsync());
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] TipoContaPagar tipoContaPagar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tipoContaPagar);
                    await _context.SaveChangesAsync();
                    TempData["MsgSucesso"] = "Cadastro efetuado com sucesso.";

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                TempData["MsgErro"] = $"Falha no cadastro. Erro: {e.Message}";
            }

            return View(tipoContaPagar);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoContaPagar == null)
            {
                return NotFound();
            }

            var tipoContaPagar = await _context.TipoContaPagar.FindAsync(id);
            if (tipoContaPagar == null)
            {
                return NotFound();
            }

            return View(tipoContaPagar);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] TipoContaPagar tipoContaPagar)
        {
            if (id != tipoContaPagar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoContaPagar);
                    await _context.SaveChangesAsync();
                    TempData["MsgSucesso"] = "Atualização efetuada com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoContaPagarExists(tipoContaPagar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    TempData["MsgErro"] = $"Falha na atualização. Erro: {e.Message}";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(tipoContaPagar);
        }

        private bool TipoContaPagarExists(int id)
        {
            return _context.TipoContaPagar.Any(e => e.Id == id);
        }
    }
}

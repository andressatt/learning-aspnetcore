using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Contas.Data;
using Lab.Contas.Models;
using static Lab.Contas.Models.Util;

namespace Lab.Contas.Controllers
{
    public class ContaPagarController : Controller
    {
        private readonly LabContasContext _context;

        public ContaPagarController(LabContasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            return View(await _context.ContaPagar.ToListAsync());
        }

        // GET: ContaPagar
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContaPagar
                .OrderByDescending(x => x.Vencimento)
                .ThenByDescending(x => x.Valor)
                .ToListAsync());
        }

        // GET: ContaPagar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContaPagar == null)
            {
                return NotFound();
            }

            var contaPagar = await _context.ContaPagar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            return View(contaPagar);
        }

        // GET: ContaPagar/Create
        public IActionResult Create()
        {
            PopularViewBags();
            return View();
        }

        // POST: ContaPagar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Valor,Vencimento,Observacoes,Status")] ContaPagar contaPagar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaPagar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopularViewBags();
            return View(contaPagar);
        }

        // GET: ContaPagar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContaPagar == null)
            {
                return NotFound();
            }

            var contaPagar = await _context.ContaPagar.FindAsync(id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            PopularViewBags();
            return View(contaPagar);
        }

        // POST: ContaPagar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Valor,Vencimento,Observacoes,Status")] ContaPagar contaPagar)
        {
            if (id != contaPagar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaPagar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaPagarExists(contaPagar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            PopularViewBags();
            return View(contaPagar);
        }

        // GET: ContaPagar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContaPagar == null)
            {
                return NotFound();
            }

            var contaPagar = await _context.ContaPagar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            return View(contaPagar);
        }

        // POST: ContaPagar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContaPagar == null)
            {
                return Problem("Entity set 'LabContasContext.ContaPagar'  is null.");
            }
            var contaPagar = await _context.ContaPagar.FindAsync(id);
            if (contaPagar != null)
            {
                _context.ContaPagar.Remove(contaPagar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaPagarExists(int id)
        {
            return _context.ContaPagar.Any(e => e.Id == id);
        }

        private void PopularViewBags()
        {
            ViewBag.TiposConta =
                new SelectList(
                    Enum.GetValues(typeof(TipoConta)).Cast<TipoConta>().ToDictionary(x => (int)x, x => x.GetDescription()),
                    "Key", "Value"
                ).OrderBy(x => x.Text);
            ViewBag.StatusContaPagar =
                new SelectList(
                    Enum.GetValues(typeof(StatusContaPagar)).Cast<StatusContaPagar>().ToDictionary(x => (int)x, x => x.GetDescription()),
                    "Key", "Value"
                ).OrderBy(x => x.Text);
        }
    }
}

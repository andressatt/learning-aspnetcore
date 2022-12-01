using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Contas.Data;
using Lab.Contas.Models;
using static Lab.Contas.Models.Util;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace Lab.Contas.Controllers
{
    public class ContaPagarController : Controller
    {
        private readonly LabContasContext _context;

        public ContaPagarController(LabContasContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Dashboard(string anoMes)
        {
            var contasPagar = await _context.ContaPagar.Include(x => x.Tipo).ToListAsync();

            var mesSelecionado = DateTime.Now;
            if (!string.IsNullOrEmpty(anoMes) && Regex.IsMatch(anoMes, "[0-9]{4}-[0-9]{1,2}"))
            {
                if (int.TryParse(anoMes.AsSpan(0, 4), out int ano) && int.TryParse(anoMes.AsSpan(5), out int mes) && mes >= 1 && mes <= 12)
                {
                    mesSelecionado = new DateTime(ano, mes, 01);
                }
            }
            var mesAnterior = mesSelecionado.AddMonths(-1);

            var contasMesAtual = contasPagar
                .Where(x => x.Vencimento.Month == mesSelecionado.Month && x.Vencimento.Year == mesSelecionado.Year)
                .OrderByDescending(x => x.Vencimento).ThenByDescending(x => x.Valor)
                .ToList();
            decimal somaMesAtual = contasMesAtual.Sum(x => x.Valor);
            decimal somaPagoMesAtual = contasMesAtual.Where(x => x.Status == StatusContaPagar.Pago).Sum(x => x.Valor);
            decimal somaNaoPagoMesAtual = contasMesAtual.Where(x => x.Status == StatusContaPagar.NaoPago).Sum(x => x.Valor);

            var contasMesAnterior = contasPagar
                .Where(x => x.Vencimento.Month == mesAnterior.Month && x.Vencimento.Year == mesAnterior.Year)
                .ToList();
            decimal somaMesAnterior = contasMesAnterior.Sum(x => x.Valor);

            decimal diffMes =
                somaMesAnterior == 0 && somaMesAtual == 0 ? 0
                : somaMesAnterior > 0 ? (somaMesAtual / somaMesAnterior) - 1
                : 1;

            var contasParaGrafico = contasMesAtual
                .OrderByDescending(x => x.Valor)
                .ThenBy(x => x.Tipo.Descricao)
                .Select(x => new { value = x.Valor, name = x.Tipo.Descricao })
                .ToList();

            return View(new ContaPagarViewModel
            {
                Ano = mesSelecionado.Year,
                Mes = mesSelecionado.Month,
                ListaContasPagarMes = contasMesAtual,
                TotalPagar = somaMesAtual,
                TotalPago = somaPagoMesAtual,
                TotalNaoPago = somaNaoPagoMesAtual,
                DiferencaMes = diffMes,
                ContasPagarParaGrafico = JsonSerializer.Serialize(contasParaGrafico)
            });
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContaPagar
                .Include(x => x.Tipo)
                .ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContaPagar == null)
            {
                return NotFound();
            }

            var contaPagar = _context.ContaPagar.Include(x => x.Tipo).ToList().Find(m => m.Id == id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            return View(contaPagar);
        }

        [Authorize]
        public IActionResult Create()
        {
            PopularViewBags();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Valor,Vencimento,Observacoes,Status")] ContaPagar contaPagar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (contaPagar.Tipo != null)
                        contaPagar.Tipo = await _context.TipoContaPagar.FindAsync(contaPagar.Tipo.Id);

                    if (ContaPagarDuplicada(contaPagar))
                    {
                        TempData["MsgErro"] = "Conta de mesmo Tipo, Valor e Vencimento já cadastrada.";
                        PopularViewBags();
                        return View(contaPagar);
                    }
                    _context.Add(contaPagar);
                    await _context.SaveChangesAsync();
                    TempData["MsgSucesso"] = "Cadastro efetuado com sucesso.";

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                TempData["MsgErro"] = $"Falha no cadastro. Erro: {e.Message}";
            }

            PopularViewBags();
            return View(contaPagar);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContaPagar == null)
            {
                return NotFound();
            }

            var contaPagar = _context.ContaPagar.Include(x => x.Tipo).ToList().Find(x => x.Id == id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            PopularViewBags();
            return View(contaPagar);
        }

        [Authorize]
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
                    if (contaPagar.Tipo != null)
                        contaPagar.Tipo = await _context.TipoContaPagar.FindAsync(contaPagar.Tipo.Id);

                    if (ContaPagarDuplicada(contaPagar))
                    {
                        TempData["MsgErro"] = "Conta de mesmo Tipo, Valor e Vencimento já cadastrada.";
                        PopularViewBags();
                        return View(contaPagar);
                    }
                    _context.Update(contaPagar);
                    await _context.SaveChangesAsync();
                    TempData["MsgSucesso"] = "Atualização efetuada com sucesso.";
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
                catch (Exception e)
                {
                    TempData["MsgErro"] = $"Falha na atualização. Erro: {e.Message}";
                }
                return RedirectToAction(nameof(Index));
            }

            PopularViewBags();
            return View(contaPagar);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContaPagar == null)
            {
                return NotFound();
            }

            var contaPagar = await _context.ContaPagar.Include(x => x.Tipo).FirstOrDefaultAsync(m => m.Id == id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            return View(contaPagar);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
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
                TempData["MsgSucesso"] = "Exclusão efetuada com sucesso.";
            }
            catch (Exception e)
            {
                TempData["MsgErro"] = $"Falha na exclusão. Erro: {e.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        #region Métodos auxiliares

        private bool ContaPagarExists(int id)
        {
            return _context.ContaPagar.Any(e => e.Id == id);
        }

        private bool ContaPagarDuplicada(ContaPagar contaPagar)
        {
            return _context.ContaPagar
                .Include(x => x.Tipo)
                .Any(x => x.Id != contaPagar.Id && x.Tipo == contaPagar.Tipo && x.Valor == contaPagar.Valor && x.Vencimento == contaPagar.Vencimento);
        }

        private void PopularViewBags()
        {
            ViewBag.TiposConta =
                new SelectList(
                    _context.TipoContaPagar.ToDictionary(x => x.Id, x => x.Descricao),
                    "Key", "Value"
                ).OrderBy(x => x.Text);
            ViewBag.StatusContaPagar =
                new SelectList(
                    Enum.GetValues(typeof(StatusContaPagar)).Cast<StatusContaPagar>().ToDictionary(x => (int)x, x => x.GetDescription()),
                    "Key", "Value"
                ).OrderBy(x => x.Text);
        }

        #endregion
    }
}

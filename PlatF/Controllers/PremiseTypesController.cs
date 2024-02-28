using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Entities;

namespace PlatF.Controllers
{
    public class PremiseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PremiseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PremiseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PremiseTypes.ToListAsync());
        }

        // GET: PremiseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premiseType = await _context.PremiseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premiseType == null)
            {
                return NotFound();
            }

            return View(premiseType);
        }

        // GET: PremiseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PremiseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsActive,Code,Id,EntryDate,IsDeleted")] PremiseType premiseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(premiseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(premiseType);
        }

        // GET: PremiseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premiseType = await _context.PremiseTypes.FindAsync(id);
            if (premiseType == null)
            {
                return NotFound();
            }
            return View(premiseType);
        }

        // POST: PremiseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsActive,Code,Id,EntryDate,IsDeleted")] PremiseType premiseType)
        {
            if (id != premiseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(premiseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PremiseTypeExists(premiseType.Id))
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
            return View(premiseType);
        }

        // GET: PremiseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premiseType = await _context.PremiseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premiseType == null)
            {
                return NotFound();
            }

            return View(premiseType);
        }

        // POST: PremiseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var premiseType = await _context.PremiseTypes.FindAsync(id);
            _context.PremiseTypes.Remove(premiseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PremiseTypeExists(int id)
        {
            return _context.PremiseTypes.Any(e => e.Id == id);
        }
    }
}

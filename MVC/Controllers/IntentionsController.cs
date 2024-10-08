using Agro.Model.Data;
using Agro.Model.Dto.Intention;
using Agro.Model.Entities;
using Agro.Model.Enums;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.EnumExtension;

namespace MVC.Controllers
{
    public class IntentionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IIntentionService _intentionService;

        public IntentionsController(ApplicationDbContext context, IIntentionService intentionService)
        {
            _context = context;
            _intentionService = intentionService;

    }

    // GET: Intentions
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _intentionService.GetAllPagedAsync(page));
        }

        // GET: Intentions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intention = await _context.Intentions
                .Include(i => i.City)
                .Include(i => i.Request)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intention == null)
            {
                return NotFound();
            }

            return View(intention);
        }

        [Authorize] 
        public IActionResult Create()
        {
            //ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            //var animalParts = from AnimalPart part in Enum.GetValues(typeof(AnimalPart))
            //                 select new { Id = (int)part, Name = EnumExtensions.ToDescription(part) };
            //ViewData["AnimalPart"] = new SelectList(animalParts, "Id", "Name", AnimalPart.Whole );
            PrepareDataForCityAndAnimalPart();
            return View();
        }

        private void PrepareDataForCityAndAnimalPart()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            var animalParts = from AnimalPart part in Enum.GetValues(typeof(AnimalPart))
                              select new { Id = (int)part, Name = EnumExtensions.ToDescription(part) };
            ViewData["AnimalPart"] = new SelectList(animalParts, "Id", "Name", AnimalPart.Whole);
        }

        // POST: Intentions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //public async Task<IActionResult> Create([Bind("Name,UserId,CityId,RequestId,Message,IntentionStatus,AnimalPart,IsActive,Code,Id,EntryDate,IsDeleted")] Intention intention)
        public async Task<IActionResult> Create([Bind("Name,CityId,Message,IntentionStatus,AnimalPart")] IntentionDto intentionDto)

        {
            if (ModelState.IsValid)
            {
                //_context.Add(intention);
                //await _context.SaveChangesAsync();
                await _intentionService.CreateAsync(intentionDto);

                return RedirectToAction(nameof(Index));
            }
            PrepareDataForCityAndAnimalPart();

            return View(intentionDto);
        }

        // GET: Intentions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var intention = await _context.Intentions.FindAsync(id);

            var intention = await _intentionService.GetIntentionByIdAsync(id.Value);
            if (intention == null)
            {
                return NotFound();
            }
            PrepareDataForCityAndAnimalPart();
            return View(intention);
        }

        // POST: Intentions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,UserId,CityId,RequestId,Message,IntentionStatus,AnimalPart,IsActive,Code,Id,EntryDate,IsDeleted")] Intention intention)
        {
            if (id != intention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntentionExists(intention.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", intention.CityId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Id", intention.RequestId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", intention.UserId);
            return View(intention);
        }

        // GET: Intentions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intention = await _context.Intentions
                .Include(i => i.City)
                .Include(i => i.Request)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intention == null)
            {
                return NotFound();
            }

            return View(intention);
        }

        // POST: Intentions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intention = await _context.Intentions.FindAsync(id);
            if (intention != null)
            {
                _context.Intentions.Remove(intention);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntentionExists(int id)
        {
            return _context.Intentions.Any(e => e.Id == id);
        }
    }
}

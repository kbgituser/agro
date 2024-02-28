using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using PlatF.Model.Interfaces;

namespace PlatF.Controllers
{
    public class PremisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUnitOfWork _unitOfWork;
        private RoleManager<IdentityRole> _roleManager;

        public PremisesController(ApplicationDbContext context
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IUnitOfWork unitOfWork
            , IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Premises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Premises
                .Include(p => p.City)
                .Include(p => p.Mall)
                .Include(p => p.PremiseType)
                .Include(p => p.User);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Premises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premise = await _context.Premises
                .Include(p => p.City)
                .Include(p => p.Mall)
                .Include(p => p.PremiseType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premise == null)
            {
                return NotFound();
            }

            return View(premise);
        }


        // GET: Premises/Create
        [Authorize]
        public IActionResult Create()
        {
            var premise = new Premise();
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["MallId"] = new SelectList(_context.Malls, "Id", "Name");
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "Id", "Name");
            return View(premise);
        }

        // POST: Premises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Floor,MallId,Area,IsLastFloor,HasWindow,Description,Price,InstaPhoto,IsSeen,PremiseTypeId,UserId,CityId,Name,IsActive,Code,Id,EntryDate,IsDeleted")] Premise premise)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("EntryDate");
            ModelState.Remove("PremiseTypeId");
            ModelState.Remove("MallId");


            if (ModelState.IsValid)
            {
                premise.EntryDate = DateTime.Now;
                var currentUser = await _userManager.GetUserAsync(User);
                premise.UserId = currentUser.Id;
                _context.Add(premise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["MallId"] = new SelectList(_context.Malls, "Id", "Name", "Торговый центр не выбран");
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "Id", "Name", "Тип помещения не выбран");

            return View(premise);
        }

        // GET: Premises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ModelState.Remove("UserId");
            ModelState.Remove("EntryDate");
            ModelState.Remove("PremiseTypeId");
            ModelState.Remove("MallId");

            var premise = await _context.Premises.FindAsync(id);
            if (premise == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", premise.CityId);
            ViewData["MallId"] = new SelectList(_context.Malls, "Id", "Name", premise.MallId);
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "Id", "Name", premise.PremiseTypeId);
            
            return View(premise);
        }

        // POST: Premises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Number,Floor,MallId,Area,IsLastFloor,HasWindow,Description,Price,InstaPhoto,IsSeen,PremiseTypeId,UserId,CityId,Name,IsActive,Code,Id,EntryDate,IsDeleted")] Premise premise)
        {
            if (id != premise.Id)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("EntryDate");

            ModelState.Remove("PremiseTypeId");
            ModelState.Remove("MallId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(premise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PremiseExists(premise.Id))
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

            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", premise.CityId);
            ViewData["MallId"] = new SelectList(_context.Malls, "Id", "Name", premise.MallId);
            ViewData["PremiseTypeId"] = new SelectList(_context.PremiseTypes, "Id", "Name", premise.PremiseTypeId);
            
            return View(premise);
        }

        // GET: Premises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premise = await _context.Premises
                .Include(p => p.City)
                .Include(p => p.Mall)
                .Include(p => p.PremiseType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premise == null)
            {
                return NotFound();
            }

            return View(premise);
        }

        // POST: Premises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var premise = await _context.Premises.FindAsync(id);
            _context.Premises.Remove(premise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PremiseExists(int id)
        {
            return _context.Premises.Any(e => e.Id == id);
        }
    }
}

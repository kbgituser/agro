using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agro.Extension;
using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using Agro.PaginatedList;

namespace Agro.Controllers
{
    public class MallsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUnitOfWork _unitOfWork;
        private RoleManager<IdentityRole> _roleManager;

        public MallsController(ApplicationDbContext context
            , UserManager<ApplicationUser> userManager            
            , RoleManager<IdentityRole> roleManager
            , IUnitOfWork unitOfWork
            , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Malls
        public async Task<IActionResult> Index(string getMine, int? p, string forAdmin, string premiseCreate)
        {
            
            var malls = _context.Malls.OrderBy(m => m.Name).AsQueryable();
            var currentUser = await _userManager.GetUserAsync(User);

            int pageSize = 1;
            int pN = (p ?? 1);

            ViewBag.PageNo = pN;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = malls.Count();

            if (currentUser!=null && (await _userManager.IsInRoleAsync(currentUser, "Admin") || await _userManager.IsInRoleAsync(currentUser, "Moderator")))
            {
                return View("IndexAdmin", await PaginatedList<Mall>.CreateAsync(malls.AsNoTracking(), pN, pageSize));
            }
            else
            {
                return View(await PaginatedList<Mall>.CreateAsync(malls.AsNoTracking(), pN, pageSize));
            }
        }

        // GET: Malls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mall = await _context.Malls
                .Include(m => m.City)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mall == null)
            {
                return NotFound();
            }

            return View(mall);
        }

        [Authorize]
        // GET: Malls/Create
        public IActionResult Create()
        {
            Mall mall = new Mall();
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View(mall);
        }

        // POST: Malls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,NumberOfFloors,PhoneNumber,ParkingExists,ParkingInsideExists,ParkingPayment,ParkingInsidePayment,CityId,Smprice,IsActive,Code,Id,IsDeleted")] Mall mall)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("EntryDate");
            if (ModelState.IsValid)
            {
                var currentUser = User.GetUserId();
                mall.UserId = currentUser;
                
                mall.EntryDate = DateTime.Now;
                _context.Add(mall);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", mall.CityId);            
            return View(mall);
        }

        // GET: Malls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mall = await _context.Malls.FindAsync(id);
            if (mall == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", mall.CityId);
            
            return View(mall);
        }

        // POST: Malls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Address,NumberOfFloors,PhoneNumber,ParkingExists,ParkingInsideExists,ParkingPayment,ParkingInsidePayment,CityId,Smprice,IsActive,Code,Id,EntryDate,IsDeleted")] Mall mall)
        {
            if (id != mall.Id)
            {
                return NotFound();
            }
            ModelState.Remove("UserId");
            ModelState.Remove("EntryDate");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MallExists(mall.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", mall.CityId);
            
            return View(mall);
        }

        // GET: Malls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mall = await _context.Malls
                .Include(m => m.City)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mall == null)
            {
                return NotFound();
            }

            return View(mall);
        }

        // POST: Malls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mall = await _context.Malls.FindAsync(id);
            _context.Malls.Remove(mall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MallExists(int id)
        {
            return _context.Malls.Any(e => e.Id == id);
        }

        // to do make getting currentuser in separate service
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = user?.Value;
            var test = await _userManager.GetUserAsync(ClaimsPrincipal.Current);
            if (userId != null)
            {
                var apuser = _context.ApplicationUsers.SingleOrDefault(u => u.Id == userId);
                return apuser;
            }
            return null;
        }
    }
}

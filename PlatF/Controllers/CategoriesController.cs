using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using PlatF.Model.Extensions;

namespace PlatF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var test = await _context.Categories.ToListAsync();
            //.Include(x => x.ParentCategory)
            return View(test);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            var category = new Category();
            ViewBag.Categories = _context.Categories.Select(x=> new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(category);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsActive,Code,Id,IsDeleted,ParentCategoryId")] Category category)
        {
            ModelState.Remove("ParentCategoryId");
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            

            //ViewBag.Categories = GetCategoriesWithoutDescendants(id.Value)
            //    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.Categories = GetCategoriesWithoutDescendants(id.Value)
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(category);
        }

        public IQueryable<SelectListItem> GetCategoriesExceptOne(int id)
        {
            return _context.Categories.Where(x => x.Id != id)
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
        }
        //public IQueryable<Category> GetCategoriesWithoutDescendants(int id)
        //{
        //    //works only for 3 level
        //    //var directDescendants = _context.Categories.Where(x => x.ParentCategoryId == id).ToList();
        //    var currentCategory = _context.Categories
        //        .Include(x => x.ChildrenCategories)
        //        .ThenInclude(x => x.ChildrenCategories)
        //        .FirstOrDefault(x => x.Id == id);

        //    var directDescendants = currentCategory
        //        .ChildrenCategories;

        //    var directDescendantIds = directDescendants.Select(x => x.Id);
            
        //    var allDescendants = new  List<Category>();
        //    foreach (var d in directDescendants)
        //    {
        //        //var children = _context.Categories.Where(x => x.ParentCategoryId == d);
        //        var children = d.ChildrenCategories;
        //        allDescendants.AddRange(children);
        //    }
        //    allDescendants = allDescendants.Concat(directDescendants).ToList();
        //    var allDescendantIds = allDescendants.Select(x=>x.Id);

        //    var categories = _context.Categories.Where(x => 
        //        x.Id != id // filter itself
        //        && !allDescendantIds.Contains(x.Id) //filter it's children and grandchildren
        //    );
        //    return categories;
        //}

        //public IQueryable<Category> GetCategoriesWithoutDescendants2(int id)
        //{
        //    var category = _context.Categories.FirstOrDefault(x => x.Id == id);
        //    return _context.Categories.Where(x=>!x.IsMyDescendant(category));
        //}
        public IQueryable<Category> GetCategoriesWithoutDescendants(int id)
        {
            var category = _context.Categories.Include(x=>x.ChildrenCategories)
                .ThenInclude(x=>x.ChildrenCategories)
                .ThenInclude(x => x.ChildrenCategories)
                .ThenInclude(x => x.ChildrenCategories)
                .ThenInclude(x => x.ChildrenCategories)
                .FirstOrDefault(x => x.Id == id);
            var categories = category.Descendants().Where(x=>x.Id!= id);
            
            return _context.Categories.Where(x => !categories.Contains(x) && x.Id!= id);
        }
        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsActive,Code,Id,EntryDate,IsDeleted,ParentCategoryId")]  Category category)
        //public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            ModelState.Remove("ParentCategoryId");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewBag.Categories = _context.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

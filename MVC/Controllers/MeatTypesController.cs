using Agro.Logic.Services;
using Agro.Model.Dto.MeatType;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
  public class MeatTypesController : Controller
  {
    private readonly MeatTypeService _meatTypeService;

    public MeatTypesController(MeatTypeService meatTypeService)
    {
      _meatTypeService = meatTypeService;
    }

    // GET: MeatTypesController
    public async Task<ActionResult> Index()
    {
      return View(await _meatTypeService.GetAllAsync());      
    }

    // GET: MeatTypesController/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: MeatTypesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(MeatTypeDto meatTypeDto)
    {
      try
      {
        await _meatTypeService.Create(meatTypeDto);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: MeatTypesController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
      return View(await _meatTypeService.GetByIdAsync(id));
    }

    // POST: MeatTypesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(MeatTypeDto meatTypeDto)
    {
      try
      {
        await _meatTypeService.Update(meatTypeDto);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View(meatTypeDto);
      }
    }

    // GET: MeatTypesController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
      return View(await _meatTypeService.GetByIdAsync(id));
    }

    // POST: MeatTypesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, MeatTypeDto meatTypeDto)
    {
      try
      {
        await _meatTypeService.DeleteById(id);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View(await _meatTypeService.GetByIdAsync(id));
      }
    }

  }
}

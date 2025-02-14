using Agro.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class RequestsController : Controller
{
    private readonly IRequestService _requestService;

    public RequestsController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        return View(await _requestService.GetAllRequestDtoPagedAsync(page));
    }

    public async Task<IActionResult> Details(int id)
    {
        var result = await _requestService.GetRequestByIdAsync(id);
        return View(result);
    }
}
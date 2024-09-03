using Agro.Model.Dto.UserRegistration;
using Agro.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Rules;

namespace MVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser()
            {
                UserName = userRegistrationDto.PhoneNumber,
                Email = userRegistrationDto.Email,
            };
            var result = await _userManager.CreateAsync(user);
            
            if (result.Succeeded)
            {
                // Логика после успешной регистрации
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(userRegistrationDto);
    }
    
}

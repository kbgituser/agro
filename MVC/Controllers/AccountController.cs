﻿using Agro.Model.Dto.UserRegistration;
using Agro.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                PhoneNumber = userRegistrationDto.PhoneNumber,
                UserName = userRegistrationDto.PhoneNumber,
                Name = userRegistrationDto.PhoneNumber,
                Email = userRegistrationDto.Email,
                NormalizedEmail = userRegistrationDto.Email,
                EmailConfirmed = true
            };
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, userRegistrationDto.Password);
            user.PasswordHash = hashed;

            var result = await _userManager.CreateAsync(user);
            string[] roles = { "Buyer" };
            await _userManager.AddToRolesAsync(user, roles);
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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

}

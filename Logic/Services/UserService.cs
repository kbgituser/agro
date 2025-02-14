using Agro.Model.Data;
using Agro.Model.Dto.User;
using Agro.Model.Dto.UserRegistration;
using Agro.Model.Entities;
using Agro.Model.Exceptions;
using Agro.Model.Interfaces;
using Agro.Model.UserRoles;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _applicationDbContext;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
    }

    public async Task<ApplicationUser> Create(UserRegistrationDto userRegistrationDto)
    {
        if (await IsUserExist(userRegistrationDto.Email))
        {
            throw new PasswordDoesnMatchException("User with indicated email already exists. Please enter different email");
        }

        //if (!string.IsNullOrEmpty(userRegistrationDto.PhoneNumber) && await IsUserExistByPhone(userRegistrationDto.PhoneNumber))
        //{
        //    throw new PasswordDoesnMatchException("Indicated phone number already exists. Please enter different phone number");
        //}

        IdentityResult result = await _userManager.CreateAsync(new ApplicationUser()
        {
            Email = userRegistrationDto.Email,
            UserName = userRegistrationDto.Email,
            PhoneNumber = userRegistrationDto.PhoneNumber,
            CreateDate = DateTime.Now
        }, userRegistrationDto.Password);
        
        

        if (result.Succeeded)
        {
            
            var newUser =  await _userManager.FindByEmailAsync(userRegistrationDto.Email);
            var userRole = _userManager.AddToRolesAsync
                           (newUser, new string[] { "User" }).Result;

            return await _userManager.FindByEmailAsync(userRegistrationDto.Email);

        }
        throw new UserException(string.Join(",", result.Errors.Select(x => x.Description)));
    }

    public async Task<ApplicationUser> CreateByPhone(string phone, string password)
    {
        if (await IsUserExistByPhone(phone))
        {
            throw new PasswordDoesnMatchException("User with indicated phone already exists. Please enter different phone");
        }
        IdentityResult result = await _userManager.CreateAsync(new ApplicationUser()
        {
            PhoneNumber = phone,
            UserName = phone,
            CreateDate = DateTime.Now
        }, password);
        return await _userManager.FindByNameAsync(phone);
    }


    public async Task<UserDto> Edit(UserDto user)
    {

        var existingUser = await _userManager.FindByIdAsync(user.Id);
        if (existingUser != null)
        {
            var editingUser = _mapper.Map<ApplicationUser>(user);
            existingUser.PhoneNumber = editingUser.PhoneNumber;
            existingUser.Email = editingUser.Email;
            existingUser.UserName = editingUser.UserName;

            await _unitOfWork.Commit();
            return _mapper.Map<UserDto>(existingUser);
        }
        throw new UserNotFoundException("User doesn't exist");
    }

    public async Task DeleteById(string id)
    {
        var deletingUser = _userManager.Users.SingleOrDefault(_ => _.Id == id);
        await _userManager.DeleteAsync(deletingUser);
        await _unitOfWork.Commit();

    }
    public List<UserDto> GetAll()
    {
        List<UserDto> applicationUsers = _mapper.Map<List<UserDto>>(_userManager.Users);
        return applicationUsers;
    }


    public async Task<bool> IsUserAndPasswordCorrect(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        PasswordVerificationResult result = _userManager.PasswordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, password);
        return result != PasswordVerificationResult.Failed;
    }

    public async Task<bool> IsUserExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false;
        }
        return true;
    }
    public async Task<bool> IsUserExistByPhone(string phoneNumber)
    {
        var user = await _userManager.FindByNameAsync(phoneNumber);
        if (user == null)
        {
            return false;
        }
        return true;
        //return _applicationDbContext.Users.Any(x=>x.PhoneNumber == phone);            
    }

    public async Task<bool> IsUserAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return await _userManager.IsInRoleAsync(user, UserRoles.Admin);
    }
}

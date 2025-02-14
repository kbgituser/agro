using Agro.Model.Dto.User;
using Agro.Model.Dto.UserRegistration;
using Agro.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Create(UserRegistrationDto userRegistrationDto);
        Task<ApplicationUser> CreateByPhone(string phone, string password);
        Task DeleteById(string id);
        Task<UserDto> Edit(UserDto user);
        List<UserDto> GetAll();
        Task<bool> IsUserAndPasswordCorrect(string email, string password);
        Task<bool> IsUserExist(string email);
        Task<bool> IsUserExistByPhone(string phoneNumber);
        Task<bool> IsUserAdmin(string userId);

    }
}
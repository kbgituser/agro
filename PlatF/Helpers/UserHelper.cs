using Microsoft.AspNetCore.Http;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlatF.Helpers
{
    public class UserHelper
    {        
        public static ApplicationUser GetCurrentUser(IHttpContextAccessor _httpContextAccessor, ApplicationDbContext _context)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user != null)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var apuser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
                    return apuser;
                }
            }

            return null;
        }
    }
}

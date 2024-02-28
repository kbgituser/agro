using PlatF.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlatF.Helpers
{
    public static class IdentityHelper
    {
        public static class IdentityHelpers
        {
            //public static ApplicationUser GetCurrentUser()
            //{
            //    var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            //    if (user != null)
            //    {
            //        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //        if (userId != null)
            //        {
            //            var apuser = _context.ApplicationUsers.SingleOrDefault(u => u.Id == userId);
            //            return apuser;
            //        }
            //    }

            //    return null;
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RolaltyCardProject.Utility
{
    public static class EasyMethods
    {
        public static string GetUserId(ClaimsPrincipal User)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            return userId;
        }
    }
}

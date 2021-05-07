using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidateUserClaim(HttpContext httpContext, string claimName, string claimValue)
        {
            return httpContext.User.Identity.IsAuthenticated && 
                httpContext.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }


    }
}

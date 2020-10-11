using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Data
{
    public class MySignInManager : SignInManager<IdentityUser>
    {
        public MySignInManager(UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<IdentityUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<IdentityUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
        public override async Task<SignInResult> PasswordSignInAsync(string login, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (login.Contains('@'))
            {
                var user = await this.UserManager.FindByEmailAsync(login);
                login = user.UserName;
            }

            return await base.PasswordSignInAsync(login, password, isPersistent, lockoutOnFailure);
        }
    }
}

using App.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WEBAPI.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AppCustomSigninManager: SignInManager<ApplicationUser>
    {
        public AppCustomSigninManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor = null, ILogger<SignInManager<ApplicationUser>> logger = null)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var result = await base.PasswordSignInAsync(username, password, true, lockoutOnFailure: false);
            // Don't do this in production, obviously!
            if (result.Succeeded)
            {
                return await Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        //public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        //{
        //    // here goes the external username and password look up

        //    if (userName.ToLower() == "username" && password.ToLower() == "password")
        //    {
        //        return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        //    }
        //    else
        //    {
        //        return Task.FromResult(SignInResult.Failed);
        //    }
        //}
    }
}

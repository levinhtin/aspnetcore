using App.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WEBAPI.Models.AccountViewModels;
using WEBAPI.Sercurity;

namespace WEBAPI.Controllers
{
    [Route("api/test")]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly TokenAuthOptions tokenOptions;

        public TokenController(TokenAuthOptions tokenOptions, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.tokenOptions = tokenOptions;
            //this.bearerOptions = options.Value;
            //this.signingCredentials = signingCredentials;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Check if currently authenticated. Will throw an exception of some sort which shoudl be caught by a general
        /// exception handler and returned to the user as a 401, if not authenticated. Will return a fresh token if
        /// the user is authenticated, which will reset the expiry.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public dynamic Get()
        {
            /* 
            ******* WARNING WARNING WARNING ****** 
            ******* WARNING WARNING WARNING ****** 
            ******* WARNING WARNING WARNING ****** 
            THIS METHOD SHOULD BE REMOVED IN PRODUCTION USE-CASES - IT ALLOWS A USER WITH 
            A VALID TOKEN TO REMAIN LOGGED IN FOREVER, WITH NO WAY OF EVER EXPIRING THEIR
            RIGHT TO USE THE APPLICATION.
            Refresh Tokens (see https://auth0.com/docs/refresh-token) should be used to 
            retrieve new tokens. 
            ******* WARNING WARNING WARNING ****** 
            ******* WARNING WARNING WARNING ****** 
            ******* WARNING WARNING WARNING ****** 
            */
            bool authenticated = false;
            string user = null;
            int entityId = -1;
            string token = null;
            DateTime? tokenExpires = default(DateTime?);

            var currentUser = HttpContext.User;
            if (currentUser != null)
            {
                authenticated = currentUser.Identity.IsAuthenticated;
                if (authenticated)
                {
                    user = currentUser.Identity.Name;
                    foreach (Claim c in currentUser.Claims) if (c.Type == "EntityID") entityId = Convert.ToInt32(c.Value);
                    tokenExpires = DateTime.UtcNow.AddMinutes(2);
                    token = GetToken(currentUser.Identity.Name, tokenExpires);
                }
            }
            return new { authenticated = authenticated, user = user, entityId = entityId, token = token, tokenExpires = tokenExpires };
        }

        public class AuthRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Request a new token for a given username/password pair.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Post(AuthRequest req)
        {
            req.UserName = "Admin";
            req.Password = "123123";
            // Obviously, at this point you need to validate the username and password against whatever system you wish.
            if ((req.UserName == "TEST" && req.Password == "TEST") || (req.UserName == "Admin" && req.Password == "123123"))
            {
                DateTime? expires = DateTime.UtcNow.AddMinutes(20);
                var token = GetToken(req.UserName, expires);
                return new { authenticated = true, entityId = 1, token = token, tokenExpires = expires };
            }
            return new { authenticated = false };
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation(3, "User created a new account with password.");
                    //return RedirectToAction(nameof(HomeController.Index), "Home");
                    DateTime? expires = DateTime.UtcNow.AddMinutes(20);
                    var token = GetToken(user.UserName, expires);
                    return Json(new { authenticated = true, entityId = 1, token = token, tokenExpires = expires });
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private string GetToken(string user, DateTime? expires)
        {
            var handler = new JwtSecurityTokenHandler();

            // Here, you should create or look up an identity for the user which is being authenticated.
            // For now, just creating a simple generic identity.
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user, "TokenAuth"), new[] { new Claim("EntityID", "1", ClaimValueTypes.Integer) });
            
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                SigningCredentials = tokenOptions.SigningCredentials,
                Subject = identity,
                Expires = expires
            });

            return handler.WriteToken(securityToken);
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using WEBAPI.ViewModels.Account;
using Microsoft.AspNet.Identity;
using WEBAPI.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net;
using System.Web.Http;

using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
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
                    _logger.LogInformation(3, "User created a new account with password.");
                    //return RedirectToAction(nameof(HomeController.Index), "Home");
                    return Ok(new OperationResult<RegisterViewModel>(OperationStatus.Success, "Register Successful", model));
                }
                else {
                    AddErrors(result);
                    return Ok(new OperationResult<string>(OperationStatus.Failure, result.Errors.FirstOrDefault().Code, result.Errors.FirstOrDefault().Description));
                }
            }

            // If we got this far, something failed, redisplay form
            return new ObjectResult(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            var result = await _userManager.Users.ToListAsync();
            return Ok(result);
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> /*Task<HttpResponseMessage>*/ Login(LoginViewModel model, string returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    //return RedirectToLocal(returnUrl);
                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                    return new ObjectResult(result);

                }
                if (result.RequiresTwoFactor)
                {
                    //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    //return View("Lockout");
                    //return Request.CreateResponse(HttpStatusCode.OK, model);
                    return new ObjectResult(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    //return View(model);
                    //return Request.CreateResponse(HttpStatusCode.OK, model);
                    return new ObjectResult(model);
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(model);
            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nobody Registered :(");
            return new ObjectResult(model);

        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [Route("logoff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            //return RedirectToAction(nameof(HomeController.Index), "Home");
            return null;
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
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                return null;
            }
        }

        #endregion
    }
}

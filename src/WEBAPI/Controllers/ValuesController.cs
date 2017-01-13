using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Data.Repository.Blog;
using App.Data.Entities.Blog;
using App.Data.Repository;
using Microsoft.AspNetCore.Identity;
using App.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.SwaggerGen.Annotations;

namespace WEBAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ValuesController : Controller
    {
        private IArticleRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public ValuesController(IArticleRepository repository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager /*, IRepository<Category> repositoryCtg*/)
        {
            //_dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            //_repositoryCtg = repositoryCtg;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize] 
        [Produces(typeof(Article))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(Article))]
        public async Task<IEnumerable<Article>> Get()
        {
            try
            {
                //var result = await _signInManager.PasswordSignInAsync("levinhtin@gmail.com", "123123", true, true);
                //var c = _repository.AllArticles;
                //var test = _dbContext.Articles.ToList();
                var admin = await _userManager.FindByNameAsync("Admin");
                return await _repository.GetAllAsync();
                //var test3 = _repositoryCtg.GetAllAsync();
                //return test2;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces(typeof(Article))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(Article))]
        public async Task<IActionResult> Post(Article model)
        {
            return Ok(model);
        }

        [HttpGet("claims")]
        [Authorize(Roles = "Manager")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c =>
                        new
                        {
                            Type = c.Type,
                            Value = c.Value
                        }));
        }
    }
}
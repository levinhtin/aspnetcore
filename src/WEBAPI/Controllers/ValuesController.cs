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

namespace WEBAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ValuesController : Controller
    {
        private IArticleRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ValuesController(IArticleRepository repository, UserManager<ApplicationUser> userManager /*, IRepository<Category> repositoryCtg*/)
        {
            //_dbContext = dbContext;
            _userManager = userManager;
            _repository = repository;
            //_repositoryCtg = repositoryCtg;
        }
        //// GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Article>> Get()
        {
            //var c = _repository.AllArticles;
            //var test = _dbContext.Articles.ToList();
            var admin = await _userManager.FindByNameAsync("Admin");
            return await _repository.GetAllAsync();
            //var test3 = _repositoryCtg.GetAllAsync();
            //return test2;
        }

    }
}
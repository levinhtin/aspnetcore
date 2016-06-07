using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Data.BlogRepository;
using WEBAPI.Data;
using WEBAPI.Data.Entities;
using WEBAPI.Data.Entities.Repository;

namespace WEBAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        private IArticleRepository _repository;

        public ValuesController(ApplicationDbContext dbContext, IArticleRepository repository/*, IRepository<Category> repositoryCtg*/)
        {
            //_dbContext = dbContext;
            _repository = repository;
            //_repositoryCtg = repositoryCtg;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Article>> Get()
        {
            //var c = _repository.AllArticles;
            //var test = _dbContext.Articles.ToList();
            return await _repository.GetAllAsync();
            //var test3 = _repositoryCtg.GetAllAsync();
            //return test2;
        }

    }
}
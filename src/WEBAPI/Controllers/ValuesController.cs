using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using WEBAPI.Repository;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private IArticleRepository _repository;
        //private IRepository<Category> _repositoryCtg;
        public ValuesController(ApplicationDbContext dbContext, IArticleRepository repository/*, IRepository<Category> repositoryCtg*/)
        {
            _dbContext = dbContext;
            _repository = repository;
            //_repositoryCtg = repositoryCtg;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var c = _repository.AllArticles;
            var test = _dbContext.Articles.ToList();
            var test2 = _repository.AllArticles;
            //var test3 = _repositoryCtg.GetAllAsync();
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

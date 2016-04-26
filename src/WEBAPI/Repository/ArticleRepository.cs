using WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace WEBAPI.Repository
{
    public class ArticleRepository : IArticleRepository, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public ArticleRepository(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<Article> AllArticles
        {
            get
            {
                return _dbContext.Articles;
            }
        }

        public async Task<Article> Add(Article _article)
        {
            _dbContext.Articles.Add(_article);
            await _dbContext.SaveChangesAsync();
            return _article;
        }

        public async Task<Article> Delete(int id)
        {
            Article _article = await _dbContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Articles.Remove(_article);
            await _dbContext.SaveChangesAsync();
            return _article;
        }

        public async Task<Article> GetById(int id)
        {
             return await _dbContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Dispose()
        {
            if (_dbContext != null) _dbContext.Dispose();
        }
    }
}

using WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Repository
{
    public class ArticleRepository : IArticleRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public IEnumerable<Article> AllArticle
        {
            get
            {
                return _context.Articles;
            }
        }

        public void Add(Article item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Article GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
    }
}

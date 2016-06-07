using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Data.Entities;
using WEBAPI.Data.Entities.Repository;

namespace WEBAPI.Data.BlogRepository
{
    public interface IArticleRepository : IRepository<Article>
    {

    }
}

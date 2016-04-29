﻿using WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace WEBAPI.Repository
{
    public interface IArticleRepository: IArticleRepositoryReadOnly
    {
        IEnumerable<Article> AllArticles { get; }
        Task<Article> Add(Article item);
        Task<Article> GetById(int id);
        Task<Article> Delete(int id);
    }
}
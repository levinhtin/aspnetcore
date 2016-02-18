using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNETCORE.Repository;
using Microsoft.Extensions.Logging;

namespace ASPNETCORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IArticleRepository _articleRepository;

        public HomeController(IArticleRepository articleRepository, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<HomeController>();
            _articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            var articles = _articleRepository.AllArticle;
            _logger.LogWarning(articles.Count().ToString());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

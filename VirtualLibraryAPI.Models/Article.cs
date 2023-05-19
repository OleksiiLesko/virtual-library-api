using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for article
    /// </summary>
    public class Article : IArticle
    {
        /// <summary>
        /// Using article repository
        /// </summary>
        private readonly IArticle _articleRepository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Article> _logger;

        /// <summary>
        /// Constructor with article Repository and logger
        /// </summary>
        /// <param name="articleRepository"></param>
        /// <param name="logger"></param>
        public Article(IArticle articleRepository, ILogger<Article> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }
        /// <summary>
        /// Adding article from Article model
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article AddArticle(Domain.DTOs.Article article)
        {
            _logger.LogInformation($"Adding article from Article model {article}");
            return _articleRepository.AddArticle(article);
        }
        /// <summary>
        /// Add copy of a article by id  from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Copy AddCopyOfArticleById(int id)
        {
            _logger.LogInformation($"Add copy of a article by id  from Article model: CopyID {id}  ");
            return _articleRepository.AddCopyOfArticleById(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article AddCopyOfArticlesByIdResponse(int id)
        {
            _logger.LogInformation($"Add copy of a article by id for response  from Article model: CopyID {id}  ");
            return _articleRepository.AddCopyOfArticlesByIdResponse(id);
        }
        /// <summary>
        /// Deleting article from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article DeleteArticle(int id)
        {
            _logger.LogInformation($"Deleting article from Article model: ArticleID {id}");
            return _articleRepository.DeleteArticle(id);
        }
        /// <summary>
        /// Getting all articles from Article model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.Entities.Article> GetAllArticles()
        {
            _logger.LogInformation($"Getting all articles from Article model ");
            var books = _articleRepository.GetAllArticles();
            if (books.Any())
            {
                return books;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Get all articles for response DTO from Article model 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Article> GetAllArticlesResponse()
        {
            _logger.LogInformation("Get all articles for response DTO from Article model  ");
            return _articleRepository.GetAllArticlesResponse();
        }
        /// <summary>
        /// Getting article by id from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article GetArticleById(int id)
        {
            _logger.LogInformation($"Getting article by id from Article model: ArticleID {id} ");
            return _articleRepository.GetArticleById(id);
        }
        /// <summary>
        /// Get article by id for response DTO from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article GetArticleByIdResponse(int id)
        {
            _logger.LogInformation($"Get article by id for response DTO from Article model: ArticleID {id}");
            return _articleRepository.GetArticleByIdResponse(id);
        }
        /// <summary>
        /// Updating article from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article UpdateArticle(int id, Domain.DTOs.Article article)
        {
            _logger.LogInformation($"Updating article from Article model: ArticleID {id}");
            return _articleRepository.UpdateArticle(id, article);
        }
    }
}

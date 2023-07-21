using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Repository;
using VirtualLibraryAPI.Repository.Repositories;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for article
    /// </summary>
    public class ArticleModel : IArticleModel
    {
        /// <summary>
        /// Using article interface
        /// </summary>
        private readonly IArticleRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ArticleModel> _logger;

        /// <summary>
        /// Constructor with article Repository and logger
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public ArticleModel(ILogger<ArticleModel> logger, IArticleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Adding article from Article model
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article AddArticle(Domain.DTOs.Article article)
        {
            _logger.LogInformation($"Adding article from Article model {article}");
            var result = _repository.AddArticle(article);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Add copy of a article by id  from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Copy AddCopyOfArticleById(int id, bool isAvailable)
        {
            _logger.LogInformation($"Add copy of a article by id  from Article model: CopyID {id}  ");
            var result = _repository.AddCopyOfArticleById(id, isAvailable);
            if (result == null)
            {
                return result;
            }
            return result;
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
            var result = _repository.AddCopyOfArticlesByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Deleting article from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article DeleteArticle(int id)
        {
            _logger.LogInformation($"Deleting article from Article model: ArticleID {id}");
            var result = _repository.DeleteArticle(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Getting all articles from Article model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Article> GetAllArticles()
        {
            _logger.LogInformation($"Getting all articles from Article model ");
            var books = _repository.GetAllArticles();
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
            var result = _repository.GetAllArticlesResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Getting article by id from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article GetArticleById(int id)
        {
            _logger.LogInformation($"Getting article by id from Article model: ArticleID {id} ");
            var result = _repository.GetArticleById(id);
            if (result == null)
            {
                return result;
            }
            return result;
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
            var result =  _repository.GetArticleByIdResponse(id);
            if (result == null)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Updating article from Article model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article UpdateArticle(int id, Domain.DTOs.Article article)
        {
            _logger.LogInformation($"Updating article from Article model: ArticleID {id}");
            var result = _repository.UpdateArticle(id, article);
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;
using Type = VirtualLibraryAPI.Domain.Entities.Type;

namespace VirtualLibraryAPI.Repository.Repositories
{
    /// <summary>
    /// Article repository 
    /// </summary>
    public class Article : IArticle
    {
        /// <summary>
        /// Application context
        /// </summary>
        private readonly ApplicationContext _context;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Article> _logger;
        /// <summary>
        /// Constructor with context and logger
        /// </summary>
        /// <param name="context"></param>
        public Article(ApplicationContext context, ILogger<Article> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Add article to the database
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article AddArticle(Domain.DTOs.Article article)
        {
            var newArticle = new Domain.Entities.Article()
            {
                Author = article.Author,
                Version = article.Version,
                MagazineName = article.MagazineName,
                MagazinesIssueNumber = article.MagazinesIssueNumber
            };
            var item = new Item()
            {
                Type = Type.Article,
                Name = article.Name,
                PublishingDate = article.PublishingDate,
                Publisher = article.Publisher,
                Article = newArticle
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Adding article to the database: {ArticleID}", newArticle.ItemID);
            return newArticle;
        }
        /// <summary>
        /// Adding a copy of a article to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Copy AddCopyOfArticleById(int id, bool isAvailable)
        {
            var existingArticle = _context.Articles.Find(id);
            if (existingArticle == null)
            {
                return null;
            }

            var newCopy = new Domain.Entities.Copy
            {
                ItemID = id,
                IsAvailable = isAvailable
            };
            _context.Copies.Add(newCopy);
            _context.SaveChanges();

            _logger.LogInformation("Adding a copy of a article to the database: {ArticleId}", id);
            return newCopy;
        }
        /// <summary>
        ///  Add copy of a article for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article AddCopyOfArticlesByIdResponse(int id)
        {
            var result = _context.Copies
                                 .Join(_context.Articles, copy => copy.ItemID, article => article.ItemID, (copy, article) => new { Copy = copy, Article = article })
                                 .Join(_context.Items, b => b.Copy.ItemID, item => item.ItemID, (b, item) => new { Copy = b.Copy, Article = b.Article, Item = item })
                                  .Where(x => x.Item.Type == Type.Article)
                                 .Where(x => x.Copy.ItemID == id)
                                 .OrderByDescending(x => x.Copy.CopyID)
                                 .FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            _logger.LogInformation($"Add copy of a article for response: CopyID {result.Copy.CopyID}");

            return new Domain.DTOs.Article
            {
                CopyID = result.Copy.CopyID,
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                Author = result.Article.Author,
                Version = result.Article.Version,
                MagazinesIssueNumber = result.Article.MagazinesIssueNumber,
                MagazineName = result.Article.MagazineName
            };
        }
        /// <summary>
        /// Deleting article from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article DeleteArticle(int id)
        {
            var article = _context.Articles.Find(id);
            if (article == null)
            {
                return null;
            }
            _context.Articles.Remove(article);

            var item = _context.Items.FirstOrDefault(i => i.ItemID == id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            _context.SaveChanges();

            _logger.LogInformation("Deleting article from database: {ArticleID}", article.ItemID);

            return article;
        }
        /// <summary>
        /// Returning all articles from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.Entities.Article> GetAllArticles()
        {
            var articles = _context.Articles.ToList();
            _logger.LogInformation("Returning all articles from the database");

            return articles;
        }
        /// <summary>
        /// Get all articles for response DTO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Domain.DTOs.Article> GetAllArticlesResponse()
        {
            _logger.LogInformation("Get all articles for response DTO:");

            var articles = _context.Items
                .Join(_context.Articles, item => item.ItemID, article => article.ItemID, (item, article) => new { Item = item, Article = article })
                .Where(x => x.Item.Type == Type.Article)
                .Select(x => new Domain.DTOs.Article
                {
                    ArticleID = x.Item.ItemID,
                    CopyInfo = new CopyInfo
                    {
                        CountOfCopies = 0,
                        CopiesAvailability = 0
                    },
                    Name = x.Item.Name,
                    PublishingDate = x.Item.PublishingDate,
                    Publisher = x.Item.Publisher,
                    Author = x.Article.Author,
                    Version = x.Article.Version,
                    MagazinesIssueNumber = x.Article.MagazinesIssueNumber,
                    MagazineName = x.Article.MagazineName
                })
                .ToList(); 

            foreach (var article in articles)
            {
                article.CopyInfo.CountOfCopies = _context.Copies.Count(c => c.ItemID == article.ArticleID);
                article.CopyInfo.CopiesAvailability = _context.Copies.Count(c => c.ItemID == article.ArticleID && c.IsAvailable);
            }

            return articles;
        }
        /// <summary>
        /// Get article by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article GetArticleById(int id)
        {
            _logger.LogInformation($"Getting article with copies by id: ArticleID {id}");
            return _context.Articles.FirstOrDefault(b => b.ItemID == id);
        }
        /// <summary>
        /// Get article by id for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.DTOs.Article GetArticleByIdResponse(int id)
        {
            var result = _context.Items
                .Join(_context.Articles, item => item.ItemID, article => article.ItemID, (item, article) => new { Item = item, Article = article })
                .Where(x => x.Item.Type == Type.Article)
                .FirstOrDefault(x => x.Article.ItemID == id);

            if (result == null)
            {
                return null;
            }

            _logger.LogInformation($"Get article by id for response: ArticleID {id}");

            var copies = _context.Copies.Where(c => c.ItemID == result.Item.ItemID && c.IsAvailable).ToList();

            var articleDTO = new Domain.DTOs.Article
            {
                Name = result.Item.Name,
                PublishingDate = result.Item.PublishingDate,
                Publisher = result.Item.Publisher,
                Author = result.Article.Author,
                Version = result.Article.Version,
                MagazinesIssueNumber = result.Article.MagazinesIssueNumber,
                MagazineName = result.Article.MagazineName,
                CopyInfo = new CopyInfo
                {
                    CountOfCopies = GetNumberOfCopiesOfArticleById(id),
                    CopiesAvailability = copies.Count
        }
            };

            return articleDTO;
        }
        /// <summary>
        /// Update article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Domain.Entities.Article UpdateArticle(int id, Domain.DTOs.Article article)
        {
            var existingArticle = _context.Articles.Find(id);
            if (existingArticle == null)
            {
                return null;
            }
            existingArticle.Author = article.Author;
            existingArticle.MagazinesIssueNumber = article.MagazinesIssueNumber;
            existingArticle.MagazineName = article.MagazineName;
            existingArticle.Version = article.Version;
            var item = _context.Items.FirstOrDefault(i => i.ItemID == id);
            if (item == null)
            {
                return null;
            }

            item.Name = article.Name;
            item.PublishingDate = (DateTime)article.PublishingDate;
            item.Publisher = article.Publisher;

            _context.SaveChanges();
            _logger.LogInformation("Update article by id in the database: {ArticleID}", existingArticle.ItemID);
            return existingArticle;
        }
        /// <summary>
        /// Get number of articles copies
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public int GetNumberOfCopiesOfArticleById(int articleID)
        {
            _logger.LogInformation($"Getting number of copies for article with id: {articleID}");
            return _context.Copies.Count(c => c.ItemID == articleID);
        }
    }
}

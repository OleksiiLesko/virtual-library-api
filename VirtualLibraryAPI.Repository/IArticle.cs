using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Repository
{
    /// <summary>
    /// Article repsitory interface 
    /// </summary>
    public interface IArticle
    {
        /// <summary>
        /// Method for get all articles
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.Entities.Article> GetAllArticles();
        /// <summary>
        /// Method for get article by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.Article GetArticleById(int id);
        /// <summary>
        /// Method for add article
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.Entities.Article AddArticle(Domain.DTOs.Article article);
        /// <summary>
        /// Method for add copy of a article
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.Entities.Copy AddCopyOfArticleById(int id);
        /// <summary>
        /// Method for update article
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.Entities.Article UpdateArticle(int id, Domain.DTOs.Article article);
        /// <summary>
        /// Method for delete article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.Article DeleteArticle(int id);
        /// <summary>
        /// Get  article by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Article GetArticleByIdResponse(int id);
        /// <summary>
        /// Get all articles for response DTO
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Article> GetAllArticlesResponse();
        /// <summary>
        /// Add copy to article for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Article AddCopyOfArticlesByIdResponse(int id);
    }
}

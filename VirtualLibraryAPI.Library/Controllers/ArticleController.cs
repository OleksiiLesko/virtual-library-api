using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    ///  Article controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ArticleController> _logger;
        /// <summary>
        /// Article model
        /// </summary>
        private readonly IArticleModel _articleModel;
        /// <summary>
        /// Department model
        /// </summary>
        private readonly IDepartmentModel _departmentModel;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public ArticleController(ILogger<ArticleController> logger, IArticleModel articleModel, IDepartmentModel departmentModel)
        {
            _logger = logger;
            _articleModel = articleModel;
            _departmentModel = departmentModel;
        }
        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllArticles()
        {
            try
            {
                _logger.LogInformation("Get all articles ");
                var articles = _articleModel.GetAllArticles();
                if (articles == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Articles received ");
                return Ok(_articleModel.GetAllArticlesResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Add article
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddArticle([FromBody] Domain.DTOs.Article request)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var addedArticle = _articleModel.AddArticle(request);
                if (addedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding article:{ArticleID}", addedArticle.ArticleID);

                _logger.LogInformation("Article added");
                return Ok(new Domain.DTOs.Article
                {
                    ArticleID = addedArticle.ArticleID,
                    DepartmentID = addedArticle.DepartmentID,
                    Name = request.Name,
                    PublishingDate = request.PublishingDate,
                    Publisher = request.Publisher,
                    Author = request.Author,
                    Version = addedArticle.Version,
                    MagazineName = addedArticle.MagazineName,
                    MagazinesIssueNumber = addedArticle.MagazinesIssueNumber
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }

        /// <summary>
        /// Add copies of a article by id
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/Copy")]
        public IActionResult AddCopyOfArticleById(int id)
        {
            try
            {
                var addedArticle = _articleModel.AddCopyOfArticleById(id, isAvailable: true);
                if (addedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding copy:{CopyID}", id);

                _logger.LogInformation("Copy added");
                return Ok(_articleModel.AddCopyOfArticlesByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        ///  Get article by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetArticleById(int id)
        {
            try
            {
                var article = _articleModel.GetArticleById(id);

                if (article == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting article by ID:{ArticleID}", article.ArticleID);
                _logger.LogInformation("Article received ");
                return Ok(_articleModel.GetArticleByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update article by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateArticle(int id, [FromBody] Domain.DTOs.Article request)
        {
            try
            {
                var department = _departmentModel.GetDepartmentById(request.DepartmentID);
                if (department == null)
                {
                    return BadRequest("Invalid DepartmentID. Department with the specified ID does not exist.");
                }
                var updatedArticle = _articleModel.UpdateArticle(id, request);
                if (updatedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating article by ID:{ArticleID}", updatedArticle.ArticleID);
                _logger.LogInformation("Article updated ");

                return Ok(new Domain.DTOs.Article
                {
                    ArticleID = updatedArticle.ArticleID,
                    DepartmentID = updatedArticle.DepartmentID,
                    Name = request.Name,
                    Author = request.Author,
                    Version = updatedArticle.Version,
                    MagazineName = updatedArticle.MagazineName,
                    MagazinesIssueNumber = updatedArticle.MagazinesIssueNumber,
                    Publisher = request.Publisher,
                    PublishingDate = request.PublishingDate
                });
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteArticle(int id)
        {
            try
            {
                var article = _articleModel.GetArticleById(id);
                if (article == null)
                {
                    return NotFound();
                }

                _articleModel.DeleteArticle(id);
                _logger.LogInformation("Deleting article by ID:{ArticleID}", article.ArticleID);
                _logger.LogInformation("Article deleted ");

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
    }
}

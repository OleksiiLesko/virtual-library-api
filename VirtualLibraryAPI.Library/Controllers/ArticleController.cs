using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Controller for add,get,update and delete article
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
        private readonly Article _model;

        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public ArticleController(ILogger<ArticleController> logger, Article model)
        {
            _logger = logger;
            _model = model;
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
                var articles = _model.GetAllArticles();
                if (articles == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Articles received ");
                return Ok(_model.GetAllArticlesResponse());
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
                var addedArticle = _model.AddArticle(request);
                if (addedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding article:{ArticleID}", addedArticle.ItemID);

                _logger.LogInformation("Article added");
                return Ok(new Domain.DTOs.Article
                {
                    ArticleID = addedArticle.ItemID,
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
                var addedArticle = _model.AddCopyOfArticleById(id, isAvailable: true);
                if (addedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Adding copy:{CopyID}", id);

                _logger.LogInformation("Copy added");
                return Ok(_model.AddCopyOfArticlesByIdResponse(id));
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
                var article = _model.GetArticleById(id);

                if (article == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting article by ID:{ArticleID}", article.ItemID);
                _logger.LogInformation("Article received ");
                return Ok(_model.GetArticleByIdResponse(id));
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
                var updatedArticle = _model.UpdateArticle(id, request);
                if (updatedArticle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating article by ID:{ArticleID}", updatedArticle.ItemID);
                _logger.LogInformation("Article updated ");

                return Ok(new Domain.DTOs.Article
                {
                    ArticleID = updatedArticle.ItemID,
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
                var article = _model.GetArticleById(id);
                if (article == null)
                {
                    return NotFound();
                }

                _model.DeleteArticle(id);
                _logger.LogInformation("Deleting article by ID:{ArticleID}", article.ItemID);
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

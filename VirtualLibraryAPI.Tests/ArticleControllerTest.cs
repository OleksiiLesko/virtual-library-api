using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ArticleControllerTest
    {
        private readonly Mock<ILogger<ArticleController>> _loggerMock;
        private readonly Mock<ILogger<Models.ArticleModel>> _loggerArticle;
        private readonly Mock<IArticleRepository> _articleRepository;
        private readonly Models.ArticleModel _articleModelMock;
        private readonly ArticleController _articleController;

        public ArticleControllerTest()
        {
            _loggerMock = new Mock<ILogger<ArticleController>>();
            _loggerArticle = new Mock<ILogger<Models.ArticleModel>>();
            _articleRepository = new Mock<IArticleRepository>();
            _articleModelMock = new Models.ArticleModel(_loggerArticle.Object, _articleRepository.Object);
            _articleController = new ArticleController(_loggerMock.Object, _articleModelMock);
        }


        [Fact]
        public void GettAllArticles_ReturnOK()
        {
            var articles = new List<Domain.DTOs.Article>
        {
            new Domain.DTOs.Article { ArticleID = 1, Author = "Article 1" },
            new Domain.DTOs.Article { ArticleID = 2, Author = "Article 2" },
            new Domain.DTOs.Article { ArticleID = 3, Author = "Article 3" }
        };
            _articleRepository.Setup(model => model.GetAllArticles()).Returns(articles);


            var result = _articleController.GetAllArticles();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllArticles_ReturnNotFound()
        {
            _articleRepository.Setup(model => model.GetAllArticles()).Returns(new List<Domain.DTOs.Article>());

            var result = _articleController.GetAllArticles();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllArticles_ReturnbBadRequest()
        {
            _articleRepository.Setup(model => model.GetAllArticles()).Throws(new Exception("Failed to retrieve books"));

            var result = _articleController.GetAllArticles();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddArticle_ReturnOK()
        {
            var request = new Domain.DTOs.Article
            {
                Name = "Article Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            var addedArticle = new Domain.DTOs.Article
            {
                ArticleID = 1,
                MagazinesIssueNumber = "23324",
                Author = "Arnold"
            };

            _articleRepository.Setup(model => model.AddArticle(request)).Returns(addedArticle);

            var result = _articleController.AddArticle(request);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var articleResponse = Assert.IsType<Domain.DTOs.Article>(okResult.Value);
        }
        [Fact]
        public void AddArticle_ReturnNotFound()
        {
            var request = new Domain.DTOs.Article
            {
                Name = "Article Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _articleRepository.Setup(model => model.AddArticle(request)).Returns((Domain.DTOs.Article)null);

            var result = _articleController.AddArticle(request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddArticle_ReturnBadRequest()
        {
            var request = new Domain.DTOs.Article
            {
                Name = "Article Name",
                PublishingDate = DateTime.Now,
                Publisher = "Publisher"
            };

            _articleRepository.Setup(model => model.AddArticle(request)).Throws(new ArgumentException());

            var result = _articleController.AddArticle(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfArticleById_ReturnOK()
        {
            var articleId = 1;
            var isAvailable = true;
            var addedBook = new Domain.DTOs.Copy
            {
                ItemID = 2,
                CopyID = articleId
            };

            _articleRepository.Setup(model => model.AddCopyOfArticleById(articleId, isAvailable)).Returns(addedBook);

            var result = _articleController.AddCopyOfArticleById(articleId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfArticleById_ReturnNotFound()
        {
            var articleId = 1;
            var isAvailable = true;
            _articleRepository.Setup(model => model.AddCopyOfArticleById(articleId, isAvailable)).Returns((Domain.DTOs.Copy)null);

            var result = _articleController.AddCopyOfArticleById(articleId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnbBadRequest()
        {
            var articleId = 1;
            var isAvailable = true;
            _articleRepository.Setup(model => model.AddCopyOfArticleById(articleId, isAvailable)).Throws(new Exception("Some error message"));


            var result = _articleController.AddCopyOfArticleById(articleId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void GetArticleById_ReturnOK()
        {

            var articleId = 1;
            var expectedBook = new Domain.DTOs.Article
            {
                ArticleID = articleId,
                Author = "Author",
                MagazinesIssueNumber = "30/234"
            };

            _articleRepository.Setup(model => model.GetArticleById(articleId)).Returns(expectedBook);


            var result = _articleController.GetArticleById(articleId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetArticleById_ReturnNotFound()
        {
            var articleId = 1;
            _articleRepository.Setup(model => model.GetArticleById(articleId)).Returns(null as Domain.DTOs.Article);

            var result = _articleController.GetArticleById(articleId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetArticleById_ReturnbBadRequest()
        {
            var articleId = 1;
            _articleRepository.Setup(model => model.GetArticleById(articleId)).Throws(new Exception("Error retrieving book"));

            var result = _articleController.GetArticleById(articleId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateArticle_ReturnOK()
        {
            var articleId = 1;
            var request = new Domain.DTOs.Article
            {
                Name = "Updated Article Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };
            var updatedArticle = new Domain.DTOs.Article
            {
                ArticleID = articleId,
                Author = "Author",
                MagazineName = "ISBN"
            };

            _articleRepository.Setup(model => model.UpdateArticle(articleId, request)).Returns(updatedArticle);

            var result = _articleController.UpdateArticle(articleId, request);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var articleResponse = Assert.IsType<Domain.DTOs.Article>(okResult.Value);
        }
        [Fact]
        public void UpdateArticle_ReturnNotFound()
        {
            var articleId = 1;
            var request = new Domain.DTOs.Article
            {
                Name = "Updated Article Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };

            _articleRepository.Setup(model => model.UpdateArticle(articleId, request)).Returns((Domain.DTOs.Article)null);

            var result = _articleController.UpdateArticle(articleId, request);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateArticle_ReturnbBadRequest()
        {
            var articleId = 1;
            var request = new Domain.DTOs.Article
            {
                Name = "Updated Article Name",
                PublishingDate = DateTime.Now,
                Publisher = "Updated Publisher"
            };

            _articleRepository.Setup(model => model.UpdateArticle(articleId, request)).Throws(new Exception("Update failed"));

            var result = _articleController.UpdateArticle(articleId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteArticle_ReturnNoContent()
        {
            var articleId = 1;
            _articleRepository.Setup(model => model.GetArticleById(articleId)).Returns(new Domain.DTOs.Article());

            var result = _articleController.DeleteArticle(articleId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteArticle_ReturnNotFound()
        {
            var articleId = 1;
            _articleRepository.Setup(model => model.GetArticleById(articleId)).Returns((Domain.DTOs.Article)null);

            var result = _articleController.DeleteArticle(articleId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteArticle_ReturnsBadRequest_WhenExceptionThrown()
        {
            var articleId = 1;
            _articleRepository.Setup(model => model.GetArticleById(articleId)).Throws<Exception>();

            var result = _articleController.DeleteArticle(articleId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

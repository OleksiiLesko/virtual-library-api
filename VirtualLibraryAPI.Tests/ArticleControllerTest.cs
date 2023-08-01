using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Library.Controllers;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ArticleControllerTest
    {
        private readonly Mock<ILogger<ArticleController>> _logger;
        private readonly Mock<IArticleModel> _articleModel;
        private readonly Mock<IDepartmentModel> _departmentModel;
        private readonly ArticleController _controller;

        public ArticleControllerTest()
        {
            _logger = new Mock<ILogger<ArticleController>>();
            _articleModel = new Mock<IArticleModel>();
            _departmentModel = new Mock<IDepartmentModel>();
            _controller = new ArticleController(_logger.Object, _articleModel.Object, _departmentModel.Object);
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
            _articleModel.Setup(model => model.GetAllArticles()).Returns(articles);


            var result = _controller.GetAllArticles();

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GettAllArticles_ReturnNotFound()
        {
            List<Domain.DTOs.Article> articles = null;
            _articleModel.Setup(m => m.GetAllArticles()).Returns(articles);

            var result = _controller.GetAllArticles();

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GettAllArticles_ReturnbBadRequest()
        {
            _articleModel.Setup(model => model.GetAllArticles()).Throws(new Exception("Failed to retrieve books"));

            var result = _controller.GetAllArticles();

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed", badRequestResult.Value);
        }
        [Fact]
        public void AddArticle_ReturnOK()
        {
            var request = new Domain.DTOs.Article();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _articleModel.Setup(m => m.AddArticle(It.IsAny<Domain.DTOs.Article>())).Returns(new Domain.DTOs.Article { });

            var result = _controller.AddArticle(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddArticle_ReturnNotFound()
        {
            var request = new Domain.DTOs.Article();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _articleModel.Setup(m => m.AddArticle(It.IsAny<Domain.DTOs.Article>())).Returns((Domain.DTOs.Article)null);

            var result = _controller.AddArticle( request);

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

            _articleModel.Setup(model => model.AddArticle(request)).Throws(new ArgumentException());

            var result = _controller.AddArticle(request);

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

            _articleModel.Setup(model => model.AddCopyOfArticleById(articleId, isAvailable)).Returns(addedBook);

            var result = _controller.AddCopyOfArticleById(articleId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void AddCopyOfArticleById_ReturnNotFound()
        {
            var articleId = 1;
            var isAvailable = true;
            _articleModel.Setup(model => model.AddCopyOfArticleById(articleId, isAvailable)).Returns((Domain.DTOs.Copy)null);

            var result = _controller.AddCopyOfArticleById(articleId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void AddCopyOfBookById_ReturnbBadRequest()
        {
            var articleId = 1;
            var isAvailable = true;
            _articleModel.Setup(model => model.AddCopyOfArticleById(articleId, isAvailable)).Throws(new Exception("Some error message"));


            var result = _controller.AddCopyOfArticleById(articleId);

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

            _articleModel.Setup(model => model.GetArticleById(articleId)).Returns(expectedBook);


            var result = _controller.GetArticleById(articleId);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetArticleById_ReturnNotFound()
        {
            var articleId = 1;
            _articleModel.Setup(model => model.GetArticleById(articleId)).Returns(null as Domain.DTOs.Article);

            var result = _controller.GetArticleById(articleId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetArticleById_ReturnbBadRequest()
        {
            var articleId = 1;
            _articleModel.Setup(model => model.GetArticleById(articleId)).Throws(new Exception("Error retrieving book"));

            var result = _controller.GetArticleById(articleId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void UpdateArticle_ReturnOK()
        {
            int Id = 123;
            var request = new Domain.DTOs.Article();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _articleModel.Setup(m => m.UpdateArticle(It.IsAny<int>(), It.IsAny<Domain.DTOs.Article>())).Returns(new Domain.DTOs.Article { });

            var result = _controller.UpdateArticle(Id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void UpdateArticle_ReturnNotFound()
        {
            int userId = 123;
            var request = new Domain.DTOs.Article();
            var userType = UserType.Administrator;

            _departmentModel.Setup(m => m.GetDepartmentById(It.IsAny<int>())).Returns(new Domain.DTOs.Department { });

            _articleModel.Setup(m => m.UpdateArticle(It.IsAny<int>(), It.IsAny<Domain.DTOs.Article>())).Returns((Domain.DTOs.Article)null);

            var result = _controller.UpdateArticle(userId, request);

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

            _articleModel.Setup(model => model.UpdateArticle(articleId, request)).Throws(new Exception("Update failed"));

            var result = _controller.UpdateArticle(articleId, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteArticle_ReturnNoContent()
        {
            var articleId = 1;
            _articleModel.Setup(model => model.GetArticleById(articleId)).Returns(new Domain.DTOs.Article());

            var result = _controller.DeleteArticle(articleId);

            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteArticle_ReturnNotFound()
        {
            var articleId = 1;
            _articleModel.Setup(model => model.GetArticleById(articleId)).Returns((Domain.DTOs.Article)null);

            var result = _controller.DeleteArticle(articleId);

            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void DeleteArticle_ReturnsBadRequest_WhenExceptionThrown()
        {
            var articleId = 1;
            _articleModel.Setup(model => model.GetArticleById(articleId)).Throws<Exception>();

            var result = _controller.DeleteArticle(articleId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

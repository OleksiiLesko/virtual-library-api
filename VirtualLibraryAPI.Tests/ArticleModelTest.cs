using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class ArticleModelTest
    {
        private readonly ILogger<Models.ArticleModel> _logger;
        private readonly Mock<IArticleRepository> _articleRepository;

        public ArticleModelTest()
        {
            _articleRepository = new Mock<IArticleRepository>();
            _logger = new Mock<ILogger<Models.ArticleModel>>().Object;
        }

        //[Fact]
        //public void AddArticle_ReturnsAddedArticle()
        //{
        //    var articleDto = new Domain.DTOs.Article { ArticleID = 2, Version = "1.0.0", MagazineName = "Time",MagazinesIssueNumber = "40/2023" };
        //    var addedArticle = new Domain.DTOs.Article { ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
        //    _articleRepository.Setup(x => x.AddArticle(articleDto)).Returns(addedArticle);
        //    var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);

        //    var result = articleModel.AddArticle(articleDto);

        //    Assert.NotNull(result);
        //    Assert.Equal(articleDto.Version, result.Version);
        //    Assert.Equal(articleDto.MagazineName, result.MagazineName);
        //    Assert.Equal(articleDto.MagazinesIssueNumber, result.MagazinesIssueNumber);
        //    Assert.NotEqual(articleDto.ArticleID, result.ArticleID);
        //    _articleRepository.Verify(x => x.AddArticle(articleDto), Times.Once());
        //}
        //[Fact]
        //public void UpdateArticle_Should_Return_Updated_Article()
        //{
        //    var asrticleID = 1;
        //    var articleDto = new Domain.DTOs.Article { ArticleID = 2, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
        //    var updatedArticle = new Domain.DTOs.Article { ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
        //    _articleRepository.Setup(x => x.UpdateArticle(asrticleID, articleDto)).Returns(updatedArticle);
        //    var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);

        //    var result = articleModel.UpdateArticle(asrticleID, articleDto);

        //    Assert.True(result.ArticleID == 1);
        //    Assert.Equal(articleDto.Version, result.Version);
        //    Assert.Equal(articleDto.MagazineName, result.MagazineName);
        //    Assert.Equal(articleDto.MagazinesIssueNumber, result.MagazinesIssueNumber);
        //    Assert.NotEqual(articleDto.ArticleID, result.ArticleID);
        //    _articleRepository.Verify(x => x.UpdateArticle(asrticleID, articleDto), Times.Once());
        //}
        [Fact]
        public void DeleteArticle_ReturnsDeletedArticle()
        {
            int articleIdToDelete = 1;
            var expectedDeletedArticle = new Domain.DTOs.Article { ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
            _articleRepository.Setup(x => x.DeleteArticle(articleIdToDelete)).Returns(expectedDeletedArticle);
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);

            var deletedArticle = articleModel.DeleteArticle(articleIdToDelete);

            _articleRepository.Verify(x => x.DeleteArticle(articleIdToDelete), Times.Once());
            Assert.Equal(expectedDeletedArticle, deletedArticle);
        }
        [Fact]
        public void GetAllArticles_ReturnsAllArticles()
        {
            var expectedArticles = new List<Domain.DTOs.Article>
        {
            new Domain.DTOs.Article {ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" },
            new Domain.DTOs.Article { ArticleID = 2, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" },
            new Domain.DTOs.Article { ArticleID = 3, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" }
        };
            _articleRepository.Setup(x => x.GetAllArticles()).Returns(expectedArticles);
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);

            var allArticles = articleModel.GetAllArticles();

            _articleRepository.Verify(x => x.GetAllArticles(), Times.Once());
            Assert.Equal(expectedArticles, allArticles);
        }
        [Fact]
        public void GetArticleById_ReturnsCorrectArticle()
        {
            var articleId = 1;
            var expectedArticle = new Domain.DTOs.Article { ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
            _articleRepository.Setup(x => x.GetArticleById(articleId)).Returns(expectedArticle);
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);

            var article = articleModel.GetArticleById(articleId);

            _articleRepository.Verify(x => x.GetArticleById(articleId), Times.Once());
            Assert.Equal(expectedArticle, article);
        }
        [Fact]
        public void GetArticleByIdResponse_ReturnsCorrectResponseDTO()
        {

            var articleId = 1;
            var expectedArticleDTO = new Domain.DTOs.Article { ArticleID = 2, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
            _articleRepository.Setup(x => x.GetArticleByIdResponse(articleId)).Returns(expectedArticleDTO);
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);


            var articleDTO = articleModel.GetArticleByIdResponse(articleId);

            _articleRepository.Verify(x => x.GetArticleByIdResponse(articleId), Times.Once());
            Assert.Equal(expectedArticleDTO, articleDTO);
        }
        [Fact]
        public void GetAllAreticlesResponse_ReturnsExpectedResult()
        {
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);
            var expectedArticles = new List<Domain.DTOs.Article>
            {
                new Domain.DTOs.Article  { ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023"  },
                new Domain.DTOs.Article  {ArticleID = 2, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" },
                new Domain.DTOs.Article  {ArticleID = 3, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023"  }
            };

            _articleRepository.Setup(repo => repo.GetAllArticlesResponse())
                .Returns(expectedArticles);

            var result = articleModel.GetAllArticlesResponse();

            Assert.Equal(expectedArticles, result);
            _articleRepository.Verify(x => x.GetAllArticlesResponse(), Times.Once);
        }
        [Fact]
        public void AddCopyOfArticleById_ReturnsAddedCopy()
        {
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);
            var articleId = 1;
            var expectedCopyId = 2;
            var isAvailable = true;
            var expectedCopy = new Domain.DTOs.Copy { CopyID = expectedCopyId, ItemID = articleId };
            _articleRepository.Setup(x => x.AddCopyOfArticleById(articleId, isAvailable)).Returns(expectedCopy);

            var addedCopy = articleModel.AddCopyOfArticleById(articleId,isAvailable);

            Assert.NotNull(addedCopy);
            Assert.Equal(expectedCopyId, addedCopy.CopyID);
            Assert.Equal(articleId, addedCopy.ItemID);
            _articleRepository.Verify(x => x.AddCopyOfArticleById(articleId, isAvailable), Times.Once);
        }
        [Fact]
        public void AddCopyOfArticleByIdResponse_ReturnsAddedCopy()
        {
            var articleModel = new Models.ArticleModel(_logger, _articleRepository.Object);
            var articleId = 1;
            var copy = new Domain.DTOs.Article { ArticleID = 1, Version = "1.0.0", MagazineName = "Time", MagazinesIssueNumber = "40/2023" };
            _articleRepository.Setup(repo => repo.AddCopyOfArticlesByIdResponse(articleId)).Returns(copy);

            var addedCopy = articleModel.AddCopyOfArticlesByIdResponse(articleId);

            Assert.NotNull(addedCopy);
            Assert.Equal(copy.ArticleID, addedCopy.ArticleID);
            Assert.Equal(copy.Version, addedCopy.Version);
            Assert.Equal(copy.MagazineName, addedCopy.MagazineName);
            Assert.Equal(copy.MagazinesIssueNumber, addedCopy.MagazinesIssueNumber);
            _articleRepository.Verify(x => x.AddCopyOfArticlesByIdResponse(articleId), Times.Once);
        }
    }
}

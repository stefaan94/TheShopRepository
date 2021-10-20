using System.Collections.Generic;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Models;
using ArticleSupplierCatalog.Utilities;
using Autofac.Extras.Moq;
using Xunit;

namespace TheShopTests.Logic
{
    public class ArticleSupplierTests
    {
        [Fact]
        public void GetSuppliers_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDatabaseDriver>()
                    .Setup(x => x.GetArticleSupplier())
                    .Returns(GetSupplierArticlesMock());

                var cls = mock.Create<DatabaseDriver>();
                var expected = GetSupplierArticlesMock();

                var actual = cls.GetArticleSupplier();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (var i = 0; i < expected.Count; i++)
                    Assert.Equal(expected[i].NameOfSupplier, actual[i].NameOfSupplier);
            }
        }

        [Fact]
        public void GetArticles_ValidCall()
        {
            IArticleSupplier articleSupplier = new ArticleSupplier();
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IArticleSupplier>()
                    .Setup(x => x.GetFlatList())
                    .Returns(articleSupplier.GetFlatList());

                var cls = mock.Create<ArticleSupplier>();
                var expected = articleSupplier.GetFlatList();

                var actual = cls.GetFlatList();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (var i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].Id, actual[i].Id);
                    Assert.Equal(expected[i].NameOfArticle, actual[i].NameOfArticle);
                    Assert.Equal(expected[i].ArticlePrice, actual[i].ArticlePrice);
                }
            }
        }

        [Theory]
        [InlineData(1, 222, 1, 20)]
        [InlineData(2, 222, 1, 60)]
        public void CreateOrder_Successful(int articleId, int maxExpectedPrice, int buyerId, int expectedPrice)
        {
            var processor = new ArticleSupplier();
            var actual = processor.OrderArticle(articleId, maxExpectedPrice, buyerId);
            Assert.Equal(expectedPrice, actual.ArticlePrice);
        }

        [Theory]
        [InlineData(0, 222, 1)]
        [InlineData(1, 222, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        public void CreateOrder_Null(int articleId, int maxExpectedPrice, int buyerId)
        {
            var processor = new ArticleSupplier();
            var ex = Record.Exception(() => processor.OrderArticle(articleId, maxExpectedPrice, buyerId));
            Assert.Null(ex);
        }

        [Fact]
        public void SellArticle_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var article = new Article
                {
                    Id = 1,
                    NameOfArticle = "Article 1 from SupplierNo1",
                    ArticlePrice = 20
                };
                const int buyerId = 10;

                mock.Mock<IDatabaseDriver>()
                    .Setup(x => x.Add(article));

                var cls = mock.Create<ArticleSupplier>();

                cls.SellArticle(article, buyerId);

                Assert.True(article.IsSold);
            }
        }

        [Fact]
        public void SellArticle_Null()
        {
            var processor = new ArticleSupplier();
            var ex = Record.Exception(() => processor.SellArticle(new Article(), 1));
            Assert.Null(ex);
        }

        private List<Supplier> GetSupplierArticlesMock()
        {
            var articles = new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    NameOfSupplier = "SupplierNo1",
                    ListOfArticles =
                    {
                        new Article
                        {
                            Id = 1,
                            NameOfArticle = "Article 1 from SupplierNo1",
                            ArticlePrice = 20
                        },
                        new Article
                        {
                            Id = 2,
                            NameOfArticle = "Article 2 from SupplierNo1",
                            ArticlePrice = 60
                        },
                        new Article
                        {
                            Id = 3,
                            NameOfArticle = "Article 3 from SupplierNo1",
                            ArticlePrice = 23
                        }
                    }
                },
                new Supplier
                {
                    Id = 2,
                    NameOfSupplier = "SupplierNo2",
                    ListOfArticles =
                    {
                        new Article
                        {
                            Id = 1,
                            NameOfArticle = "Article 1 from SupplierNo2",
                            ArticlePrice = 140
                        },
                        new Article
                        {
                            Id = 2,
                            NameOfArticle = "Article 2 from SupplierNo2",
                            ArticlePrice = 240
                        },
                        new Article
                        {
                            Id = 3,
                            NameOfArticle = "Article 3 from SupplierNo2",
                            ArticlePrice = 99
                        }
                    }
                }
            };
            return articles;
        }
    }
}
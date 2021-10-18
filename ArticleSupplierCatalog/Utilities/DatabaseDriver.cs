using System;
using System.Collections.Generic;
using System.Linq;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Models;

namespace ArticleSupplierCatalog.Utilities
{
    public class DatabaseDriver : IDatabaseDriver
    {
        #region listArticleSupplier
        public List<Supplier> GetArticleSupplier()
        {
            var articles = GetSupplierArticles();
            return articles.ToList();
        }
        #endregion

        #region getArticleById

        public Article GetArticleById(int id)
        {
            Article output = null;
            IArticleSupplier articleSupplier = new ArticleSupplier();
            var result = articleSupplier.GetFlatList().Find(x => x.Id == id);
            if (result != null)
            {
                output = result;
            }
            else
            {
                throw new ArgumentException("Not found", nameof(id));
            }

            return output;
        }

        #endregion

        #region addArticleRegion
        public void Add(Article article)
        {
            List<Article> _articles = new List<Article>();
            if(article != null)
            {
                _articles.Add(article);
            }
            else 
            {
                throw new Exception();
            }
        }
        #endregion

        #region listOfArticleSupplier
        public List<Supplier> GetSupplierArticles()
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
        #endregion
    }
}

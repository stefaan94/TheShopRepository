using System;
using System.Collections.Generic;
using System.Linq;
using ArticleSupplierCatalog.Models;
using ArticleSupplierCatalog.Utilities;

namespace ArticleSupplierCatalog.Logic
{
    public class ArticleSupplier : IArticleSupplier
    {
        IDatabaseDriver _database = new DatabaseDriver();

        #region addArticleRegion
        public void Add(Article article)
        {
            _database.Add(article);
        }
        #endregion

        #region listOfArticles
        public List<Article> GetFlatList()
        {
            List<Article> flatList = null;
            var internalDictionary = new Dictionary<int, List<Article>>();

            foreach (var supplier in _database.GetArticleSupplier())
            {
                var articles = supplier.ListOfArticles.ToList();
                internalDictionary.Add(supplier.Id, articles);
                flatList = internalDictionary.SelectMany(d => d.Value).OrderByDescending(x => x.ArticlePrice).ToList();
            }
            return flatList;
        }
        #endregion

        #region orderRegion
        public Article OrderArticle(int articleId, int maxExpectedPrice, int buyerId)
        {
            Article article = null;
            var findMinPrice = GetFlatList().Where(x => x.Id == articleId).Min(kvp => kvp.ArticlePrice);
            foreach (var articles in GetFlatList())
            {
                if (articleId != articles.Id) continue;
                if (findMinPrice == articles.ArticlePrice)
                {
                    if (articles.IsSold) continue;
                    if (maxExpectedPrice >= articles.ArticlePrice)
                    {
                        article = articles;
                    }
                }
                else
                {
                    article = null;
                }
            }
            return article;
        }
        #endregion

        #region sellRegion
        public void SellArticle(Article articleForSale, int buyerId)
        {
            try
            {
                if (articleForSale != null)
                {
                    articleForSale.IsSold = true;
                    articleForSale.SoldDate = DateTime.Now;
                    articleForSale.BuyerUserId = buyerId;
                    Add(articleForSale);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion
    }
}

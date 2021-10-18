using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSupplierCatalog.Helpers;
using ArticleSupplierCatalog.Models;
using ArticleSupplierCatalog.Utilities;

namespace ArticleSupplierCatalog.Logic
{
    public class ArticleSupplier : IArticleSupplier
    {
        IDatabaseDriver _database;
        private Logger _logger;

        public ArticleSupplier(IDatabaseDriver database)
        {
            _database = database;
        }

        public void Add(Article article)
        {
            _database.Add(article);
        }

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

        public Article OrderArticle(int articleId, int maxExpectedPrice, int buyerId)
        {
            throw new NotImplementedException();
        }

        public void SellArticle(Article articleForSale, int buyerId)
        {
            throw new NotImplementedException();
        }
    }
}

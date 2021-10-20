using System.Collections.Generic;
using ArticleSupplierCatalog.Models;

namespace ArticleSupplierCatalog.Logic
{
    public interface IArticleSupplier
    {
        List<Article> GetFlatList();
        Article OrderArticle(int articleId, int maxExpectedPrice, int buyerId);
        void SellArticle(Article articleForSale, int buyerId);
    }
}
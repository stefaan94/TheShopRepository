using System;
using System.Linq;
using ArticleSupplierCatalog.Helpers;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Models;

namespace ArticleSupplierCatalog.Services
{
    public class ShopService
    {
        private readonly IArticleSupplier _iSupplierArticlesRepository = new ArticleSupplier();
        private readonly Logger _logger = new Logger();
        private Article _articleForSale;

        public void OrderAndSellArticle(int articleId, int maxExpectedPrice, int buyerId)
        {
            OrderArticle(articleId, maxExpectedPrice, buyerId);
            SellArticle(_articleForSale, buyerId);
        }


        #region orderRegion

        private void OrderArticle(int articleId, int maxExpectedPrice, int buyerId)
        {
            var negative = articleId < 0 || maxExpectedPrice < 0 || buyerId < 0;
            try
            {
                if (!negative)
                {
                    _logger.Debug($"Trying to sell article with id= {articleId}");
                    _articleForSale = _iSupplierArticlesRepository.OrderArticle(articleId, maxExpectedPrice, buyerId);
                    if (_articleForSale == null)
                    {
                        var findMinPrice = _iSupplierArticlesRepository.GetFlatList().Where(x => x.Id == articleId)
                            .Min(kvp => kvp.ArticlePrice);
                        if (findMinPrice >= maxExpectedPrice)
                            _logger.Error(
                                $"There are no articles of Id {articleId} found at price of {maxExpectedPrice} or less. Minimum price of requested article is {findMinPrice}");
                    }
                }
                else
                {
                    _logger.Error("Negative values are not valid.");
                }
            }
            catch (Exception)
            {
                _logger.Error(
                    $"Could not order the article with Id = {articleId} because it doesn't exist in list of articles.");
            }
        }

        #endregion

        #region sellRegion

        private void SellArticle(Article article, int buyerId)
        {
            try
            {
                if (_articleForSale != null)
                {
                    _iSupplierArticlesRepository.SellArticle(_articleForSale, buyerId);
                    _logger.Info(
                        $"Article with id= {_articleForSale.Id}, is sold with a price of {_articleForSale.ArticlePrice}," +
                        " which is the lowest price for wanted article.");
                }
            }
            catch (NullReferenceException)
            {
                _logger.Error("Could not save article with id=" + article.Id);
            }
        }

        #endregion
    }
}
using System;
using ArticleSupplierCatalog.Helpers;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Models;


namespace ArticleSupplierCatalog.Services
{
    public class ShopService
    {
        private readonly IArticleSupplier _iSupplierArticlesRepository = new ArticleSupplier();
        private readonly Logger _logger = new Logger();

        public void OrderAndSellArticle(int articleId, int maxExpectedPrice, int buyerId)
        {
            Article articleForSale;

            #region orderRegion
            try
            {
                _logger.Debug("Trying to sell article with id=" + articleId);
                articleForSale = _iSupplierArticlesRepository.OrderArticle(articleId, maxExpectedPrice, buyerId);
                if (articleForSale == null) throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Could not order article\n, None of articles meet maximum expected price");
            }
            #endregion

            #region sellRegion
            try
            {
                _iSupplierArticlesRepository.SellArticle(articleForSale, buyerId);
                _logger.Info("Article with id=" + articleForSale.Id + " is sold with a price of " +
                             articleForSale.ArticlePrice + " which is the lowest price for wanted article.");
            }
            catch (ArgumentNullException)
            {
                _logger.Error("Could not save article with id=" + articleId);
                throw new Exception("Could not save article with id" + articleId);
            }
            #endregion
        }
    }
}


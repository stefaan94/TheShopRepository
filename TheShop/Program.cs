using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Services;
using ArticleSupplierCatalog.Utilities;

namespace TheShop
{
	internal class Program
    {
        static void Main(string[] args)
        {
            var shopService = new ShopService();
            IArticleSupplier data = new ArticleSupplier();

            try
            {
                //print all articles on console
                var article = data.GetFlatList();
                var loop = article.ToList();
                Console.WriteLine("List of all articles");
                loop.ForEach(i => Console.WriteLine("Found article "
                                                    + i.NameOfArticle
                                                    + " with ID: "
                                                    + i.Id + " with a price of "
                                                    + i.ArticlePrice));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Articles not found: " + ex);
            }

            try
            {
                Console.WriteLine("Enter article Id you want to buy");
                var articleId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter maximum expected price");
                var minExpectedPrice = Convert.ToInt32(Console.ReadLine());

                //order and sell
                shopService.OrderAndSellArticle(articleId, minExpectedPrice, 10);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

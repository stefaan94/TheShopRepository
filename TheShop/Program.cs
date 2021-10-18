using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Utilities;

namespace TheShop
{
	internal class Program
    {
        static void Main(string[] args)
        {
            IDatabaseDriver driver = new DatabaseDriver();
            IArticleSupplier data = new ArticleSupplier(driver);

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
        }
    }
}

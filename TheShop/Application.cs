using System;
using System.Collections.Generic;
using ArticleSupplierCatalog.Helpers;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Models;
using ArticleSupplierCatalog.Services;
using ArticleSupplierCatalog.Utilities;

namespace TheShop
{
    public class Application : IApplication
    {
        private readonly IArticleSupplier _articleSupplierProcessor;
        private readonly IDatabaseDriver _supplierProcessor;
        private readonly Logger _logger = new Logger();
        private readonly ShopService _service = new ShopService();

        public Application(IArticleSupplier articleSupplierProcessor, IDatabaseDriver supplierProcessor)
        {
            _articleSupplierProcessor = articleSupplierProcessor;
            _supplierProcessor = supplierProcessor;
        }

        public void Run()
        {
            IdentifyNextStep();
        }

        private void IdentifyNextStep()
        {
            string selectedAction = "";

            do
            {
                selectedAction = GetActionChoice();

                Console.WriteLine();

                switch (selectedAction)
                {
                    case "1":
                        DisplayArticles(_articleSupplierProcessor.GetFlatList());
                        break;
                    case "2":
                        GetArticleSupplierById();
                        break;
                    case "3":
                        OrderAndSellArticle();
                        break;
                    case "4":
                        Console.WriteLine("Thanks for using this application");
                        break;
                    default:
                        Console.WriteLine("That was an invalid choice. Hit enter and try again.");
                        break;
                }

                Console.WriteLine("Hit return to continue...");
                Console.ReadLine();

            } while (selectedAction != "4");
        }

        private void OrderAndSellArticle()
        {
            Console.Write("Enter article Id you want to buy: ");
                var articleId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter maximum expected price: ");
                var minExpectedPrice = Convert.ToInt32(Console.ReadLine());
                var buyerId = 1;
                _service.OrderAndSellArticle(articleId, minExpectedPrice, buyerId);
        }

        private void DisplayArticles(List<Article> articles)
        {
            try
            {
                articles.ForEach(i => Console.WriteLine("Found article "
                                                        + i.NameOfArticle
                                                        + " with ID: "
                                                        + i.Id + " with a price of "
                                                        + i.ArticlePrice));
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message);
            }
        }

        private void GetArticleSupplierById()
        {
            try
            {
                Console.WriteLine("Enter Id of supplier");
                int id = Convert.ToInt32(Console.ReadLine());
                var supplier = _supplierProcessor.GetSupplierById(id);
                if (supplier != null)
                {
                    Console.WriteLine($"Supplier has been found. Id =  {supplier.Id}, name= {supplier.NameOfSupplier}");
                    foreach (var articles in supplier.ListOfArticles)
                    {
                        if (articles.IsSold == false)
                        {
                            Console.WriteLine($"This supplier has following articles in stock: Id: {articles.Id} , name: {articles.NameOfArticle}, price: {articles.ArticlePrice}");
                        }
                    }
                }
                else
                {
                    _logger.Error($"Supplier with id {id} has not been found");
                }
            }
            catch (Exception e)
            {
                throw new NullReferenceException(e.Message);
            }
        }

        private string GetActionChoice()
        {
            string output = "";

            Console.Clear();
            Console.WriteLine("Menu Options".ToUpper());
            Console.WriteLine("1 - Get list of articles");
            Console.WriteLine("2 - Get supplier by Id and his articles");
            Console.WriteLine("3 - Order article");
            Console.WriteLine("4 - Exit");
            Console.Write("What would you like to choose: ");
            output = Console.ReadLine();

            return output;
        }
    }
}
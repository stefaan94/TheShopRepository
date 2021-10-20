using System.Collections.Generic;

namespace ArticleSupplierCatalog.Models
{
    public class Supplier
    {
        public Supplier()
        {
            ListOfArticles = new List<Article>();
        }

        public int Id { get; set; }
        public string NameOfSupplier { get; set; }
        public List<Article> ListOfArticles { get; set; }
    }
}
using System.Collections.Generic;

namespace ArticleSupplierCatalog.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string NameOfSupplier { get; set; }
        public List<Article> ListOfArticles { get; set; }

        //Initialization of Supplier constructor with new list of Articles
        public Supplier()
        {
            this.ListOfArticles = new List<Article>();
        }
    }
}

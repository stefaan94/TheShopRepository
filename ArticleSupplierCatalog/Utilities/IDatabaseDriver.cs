using System.Collections.Generic;
using ArticleSupplierCatalog.Models;

namespace ArticleSupplierCatalog.Utilities
{
    public interface IDatabaseDriver
    {
        List<Supplier> GetArticleSupplier();
        Supplier GetSupplierById(int id);
        void Add(Article article);

    }
}

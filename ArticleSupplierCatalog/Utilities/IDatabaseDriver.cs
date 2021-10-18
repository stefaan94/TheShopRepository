using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSupplierCatalog.Models;

namespace ArticleSupplierCatalog.Utilities
{
    public interface IDatabaseDriver
    {
        List<Supplier> GetArticleSupplier();

        Article GetArticleById(int id);
        void Add(Article article);

    }
}

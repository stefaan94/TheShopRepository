using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Services;
using ArticleSupplierCatalog.Utilities;
using Autofac;

namespace TheShop
{
	internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}

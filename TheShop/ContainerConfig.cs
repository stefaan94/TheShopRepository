using ArticleSupplierCatalog.Logic;
using ArticleSupplierCatalog.Utilities;
using Autofac;


namespace TheShop
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<ArticleSupplier>().As<IArticleSupplier>();
            builder.RegisterType<DatabaseDriver>().As<IDatabaseDriver>();

            return builder.Build();
        }
    }
}

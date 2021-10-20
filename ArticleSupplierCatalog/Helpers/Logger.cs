using System;

namespace ArticleSupplierCatalog.Helpers
{
    #region logger

    public class Logger
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Debug(string message)
        {
            Console.WriteLine(message);
        }
    }

    #endregion
}
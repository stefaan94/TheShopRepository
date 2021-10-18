﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSupplierCatalog.Models
{
    public class Article
    { 
        public int Id { get; set; }
        public string NameOfArticle { get; set; }
        public int ArticlePrice { get; set; }
        public bool IsSold { get; set; }
        public DateTime SoldDate { get; set; }
        public int BuyerUserId { get; set; }
    }
}

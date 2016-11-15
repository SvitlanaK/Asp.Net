using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Market.Domain.Entities;

namespace Market.WEB.UI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
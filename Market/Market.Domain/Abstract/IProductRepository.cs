using System;
using System.Linq;
using System.Collections.Generic;
using Market.Domain.Entities;

namespace Market.Domain.Abstract
{
    public interface IProductRepository
    {

        IEnumerable<Product> Products {get;}
        void SaveProduct(Product product);
        Product DeleteProduct(int ProductID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Domain.Entities;
using Market.Domain.Abstract;
using Market.WEB.UI.Models;

namespace Market.WEB.UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize =6;
         public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() :
                    repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
        public FileContentResult GetImage(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if(product != null)
            {
                return File(product.ImageData, product.ImageType);
            }
            else
            {
                return null;
            }
        }
      
    }
}
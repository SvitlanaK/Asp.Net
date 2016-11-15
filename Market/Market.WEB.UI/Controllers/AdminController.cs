using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Domain.Abstract;
using Market.Domain.Entities;

namespace Market.WEB.UI.Controllers
{
    [RequireHttps]
    public class AdminController : Controller
    {
        private IProductRepository reporitory;
        public AdminController(IProductRepository r)
        {
            reporitory = r;
        }
        public ViewResult Index()
        {
            return View(reporitory.Products);
        }
        public ViewResult Edit(int productID)
        {

            Product product = reporitory.Products
                .FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image=null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                reporitory.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
           
        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deleteProduct = reporitory.DeleteProduct(productID);
            if(deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} был удален", deleteProduct.Name);
            }
            return RedirectToAction("index");
        }
    }
}
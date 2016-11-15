using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Domain.Abstract;
using Market.Domain.Entities;
using Market.WEB.UI.Models;

namespace Market.WEB.UI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository r, IOrderProcessor order)
        {
            repository = r;
            orderProcessor = order;
        }
        public ViewResult Index(Cart cart, string returnURL)
        {
            return View(new CartIndexViewModel
            {
                ReturnURL = returnURL,
                cart = cart
            });
        }
        public RedirectToRouteResult AddToCArt(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShopingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShopingDetails shopDetails)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shopDetails);
                cart.Clear();
                return View("Completed");
            }
            
            else
            {
                return View(shopDetails);
            }
        }
    }
}
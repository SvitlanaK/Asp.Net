using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Market.Domain.Entities;
using Market.Domain.Abstract;
using System.Web.Mvc;
using System.Linq;
using Market.WEB.UI.Models;
using Moq;
using Market.WEB.UI.Controllers;

namespace Market.UnitTest
{
    /// <summary>
    /// Summary description for CartTest
    /// </summary>
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_Product()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();
            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();
            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);

        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            // Act
            target.RemoveLine(p2);
            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
           
Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalVAlue();
            // Assert
            Assert.AreEqual(result, 450M);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product
            {
                ProductID = 1,
                Name = "P1",
                Price = 100M
            };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
new Product {ProductID = 1, Name = "P1", Category = "Apples"},
}.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object, null);
            // Act - add a product to the cart
            target.AddToCArt(cart, 1, null);
            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }
        [TestMethod]
public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
new Product {ProductID = 1, Name = "P1", Category = "Apples"},
}.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object, null);
            // Act - add a product to the cart
            RedirectToRouteResult result = target.AddToCArt(cart, 2, "myUrl");
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(null, null);
            // Act - call the Index action method
            CartIndexViewModel result
            = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            // Assert
            Assert.AreSame(result.cart, cart);
            Assert.AreEqual(result.ReturnURL, "myUrl");
        }
    }
}

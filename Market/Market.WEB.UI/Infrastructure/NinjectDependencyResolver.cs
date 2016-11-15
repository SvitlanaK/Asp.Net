using System;
using Ninject;
using System.Linq;
using System.Configuration;
using System.Web.Mvc;
using System.Collections.Generic;
using Moq;
using Market.Domain.Abstract;
using Market.Domain.Entities;
using Market.Domain.Concrete;
using Market.WEB.UI.Infrastructure.Abstract;
using Market.WEB.UI.Infrastructure.Concrete;

namespace Market.WEB.UI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel ninjectKernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            ninjectKernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return ninjectKernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ninjectKernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //    mock.Setup(m => m.Products).Returns(new List<Product> {
            //        new Product { Name = "GreenTee", Price = 20 },
            //        new Product { Name = "BlackTee", Price  =15 },
            //        new Product {Name = "Blackberytee", Price = 5 }
            //});
            //    ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
            //когда появилась привязка к реальной БД это нам уже не нужно
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);

            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}
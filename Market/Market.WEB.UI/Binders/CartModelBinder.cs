using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Market.Domain.Entities;

namespace Market.WEB.UI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return cart;
        }
    }
}
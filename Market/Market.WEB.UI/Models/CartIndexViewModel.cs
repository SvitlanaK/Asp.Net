using System;
using Market.Domain.Entities;

namespace Market.WEB.UI.Models
{
    public class CartIndexViewModel
    {
        public Cart cart { get; set; }
        public string ReturnURL { get; set; }
    }
}

using Market.Domain.Entities;

namespace Market.Domain.Abstract
{
  public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShopingDetails shopingDetails);
    }
}

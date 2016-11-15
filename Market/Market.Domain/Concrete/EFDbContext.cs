using System;
using Market.Domain.Entities;
using System.Data.Entity;

namespace Market.Domain.Concrete
{
  public  class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}

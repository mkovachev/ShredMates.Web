using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Models;
using System;
using System.Collections.Generic;

namespace ShredMates.Tests
{
    public class TestStartup
    {
        private static readonly bool testInitialized = false;

        public static ShredMatesDbContext GetDataBase()
        {
            var dbOptions = new DbContextOptionsBuilder<ShredMatesDbContext>()
                               .UseInMemoryDatabase("TestDb")
                               .Options;
            var db = new ShredMatesDbContext(dbOptions);

            return db;
        }

        public static ShoppingCart GetShoppingCart()
        {
            var shoppingCart = new ShoppingCart()
            {
                Id = "1",
                ShoppingCartItems = new List<ShoppingCartItem>()
            };

            return shoppingCart;
        }

        public static List<Product> GetProducts()
            => new List<Product>()
            {
                new Product { Id = 1, Title = "A", Price = 100, CategoryId = 1 },
                new Product { Id = 2, Title = "B", Price = 200, CategoryId = 1 },
                new Product { Id = 3, Title = "C", Price = 300, CategoryId = 1 }
            };

        public static Order GetOrder()
            => new Order
            {
                OrderPlaced = DateTime.MinValue
            };

        public static Category GetCategory()
            => new Category
            {
                Id = 1,
                Name = "Snowboard",
                Products = new List<Product>()
            };

        public static OrderDetail GetOrderDetails()
          => new OrderDetail
          {
              Amount = 1,
              ProductId = 1,
              OrderId = 1,
              Price = 100
          };
    }
}

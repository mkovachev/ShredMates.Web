using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;

namespace ShredMates.Tests
{
    public class TestStartup
    {
        private static bool testInitialized = false;

        //private readonly AutoMapperProfile mapper;
        //private readonly ShredMatesDbContext db;
        //private readonly ShoppingCart shoppingCart;
        //private readonly List<Product> products;
        //private readonly Category category;
        //private readonly Order order;
        //private readonly OrderDetail orderDetails;

        //public TestStartup()
        //{
        //    GetMapper();
        //    this.db = GetDataBase();
        //    this.shoppingCart = GetShoppingCart();
        //    this.products = GetProducts();
        //    this.category = GetCategory();
        //    this.order = GetOrder();

        //    foreach (var product in this.products)
        //    {
        //        var id = 1;
        //        var item = new ShoppingCartItem()
        //        {
        //            Id = id++,
        //            Product = product,
        //            Amount = 1,
        //            ShoppingCartId = "1"
        //        };
        //        shoppingCart.ShoppingCartItems.Add(item);
        //    }

        //    this.db.Products.AddRange(products);
        //    this.db.Categories.Add(category);
        //    this.db.Orders.Add(order);
        //    this.db.OrderDetails.Add(orderDetails);

        //    this.db.SaveChanges();
        //}

        public static void GetMapper()
        {
            if (!testInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
                testInitialized = true;
            }
        }

        public static ShredMatesDbContext GetDataBase()
        {
            var dbOptions = new DbContextOptionsBuilder<ShredMatesDbContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
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
                new Product { Id = 1, Title = "A", Price = 100, CategoryId = 1},
                new Product { Id = 2, Title = "B", Price = 200, CategoryId = 1},
                new Product { Id = 3, Title = "C", Price = 300, CategoryId = 1}
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

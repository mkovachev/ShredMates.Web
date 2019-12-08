﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShredMates.Tests
{
    public static class TestStartup
    {
        public static Mapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps("ShredMates.Data");
                cfg.AddMaps("ShredMates.Services");
                cfg.AddMaps("ShredMates.Web");
                cfg.CreateMap<Product, ProductListingServiceModel>();
            });

            var mapper = new Mapper(config);

            return mapper;
        }
        public static Category CreateCategory()
            => new Category
            {
                Id = 1,
                Name = "Snowboard",
                Products = new List<Product>()
            };
        public static List<Product> CreateProducts()
            => new List<Product>()
            {
                new Product { Id = 1, Title = "TestProduct1", Price = 1, CategoryId = 1 },
                new Product { Id = 2, Title = "TestProduct2", Price = 1, CategoryId = 1 },
                new Product { Id = 3, Title = "TestProduct3", Price = 1, CategoryId = 1 }
            };
        public static Order CreateOrder()
            => new Order
            {
                Id = 1,
                OrderPlaced = DateTime.MinValue
            };
        public static OrderDetail CreateOrderDetails()
          => new OrderDetail
          {
              Id = 1,
              Amount = 1,
              ProductId = 1,
              OrderId = 1,
              Price = 1
          };
        public static ShredMatesDbContext CreateDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ShredMatesDbContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;
            var db = new ShredMatesDbContext(dbOptions);

            db.Categories.Add(CreateCategory());
            db.Products.AddRange(CreateProducts());
            db.Orders.Add(CreateOrder());
            db.OrderDetails.Add(CreateOrderDetails());
            db.SaveChanges();

            return db;
        }
        public static ShoppingCart CreateShoppingCart()
        {
            var shoppingCart = new ShoppingCart()
            {
                Id = "1",
                ShoppingCartItems = new List<ShoppingCartItem>()
            };

            return shoppingCart;
        }

        public static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }

        public static string GetString(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return Encoding.UTF8.GetString(data);
        }

        public static byte[] Get(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] value);
            return value;
        }

    }
}

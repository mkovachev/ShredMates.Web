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
            => new ShoppingCart() { Id = "1" };

        public static List<Product> GetProducts()
            => new List<Product>()
            {
                ( new Product { Title = "A", CategoryId = 1}),
                 ( new Product { Title = "B", CategoryId = 1}),
                 ( new Product { Title = "C", CategoryId = 1})
            };

        public static Order GetOrder()
            => new Order
            {
                OrderPlaced = DateTime.MinValue
            };

        //public static CategoryService GetCategoryService() => new CategoryService(db, shoppingCart);
    }
}

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Services
{
    public class CategoryServiceTest
    {
        public CategoryServiceTest()
        {
            TestStartup.GetMapper();
        }

        [Fact]
        public async Task ByIdAsync_ShouldReturn_CorrectCategoryWithId()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<ShredMatesDbContext>()
                                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                   .Options;
            var db = new ShredMatesDbContext(dbOptions);
            var shoppingCart = new ShoppingCart();
            var categoryService = new CategoryService(db, shoppingCart);

            var products = new List<Product>()
            {
                ( new Product { Title = "A"}),
                 ( new Product { Title = "B"}),
                 ( new Product { Title = "C"})
            };

            var category = new Category
            {
                Id = 1,
                Name = "Snowboard",
                Products = products
            };

            db.Add(category);
            db.SaveChanges();

            // Act
            var result = await categoryService.ByIdAsync(category.Id);

            // Assert
            result.Should().BeOfType(typeof(Category));
            result.Should().NotBeNull();
            result.Should()
                .Match<Category>(c => 
                                    c.Id == category.Id
                                    && c.Name == category.Name);
        }


        [Fact]
        public async Task ProductsinCategoryAsync_ShouldReturnAllProductFromCategory_OrderedByTitle()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<ShredMatesDbContext>()
                                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                       .Options;
            var db = new ShredMatesDbContext(dbOptions);
            var shoppingCart = new ShoppingCart();
            var categoryService = new CategoryService(db, shoppingCart);

            var products = new List<Product>()
            {
                ( new Product { Title = "A"}),
                 ( new Product { Title = "B"}),
                 ( new Product { Title = "C"})
            };

            var category = new Category
            {
                Id = 1,
                Name = "Snowboard",
                Products = products
            };

            db.Add(category);
            db.SaveChanges();

            // Act
            var result = await categoryService.ProductsinCategoryAsync(category.Id);

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Title == "A"
                             && r.ElementAt(1).Title == "B")
                .And
                .HaveCount(3);
        }
    }
}

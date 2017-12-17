using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Services
{
    public class CategoryServiceTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;
        private readonly List<Product> products;

        public CategoryServiceTest()
        {
            TestStartup.GetMapper();
            this.db = TestStartup.GetDataBase();
            this.shoppingCart = TestStartup.GetShoppingCart();
            this.products = TestStartup.GetProducts();
        }

        [Fact]
        public async Task ByIdAsync_ShouldReturn_CorrectCategoryWithId()
        {
            // Arrange
            var categoryService = new CategoryService(db, shoppingCart);


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
                .Match<Category>(r =>
                                    r.Id == category.Id
                                    && r.Name == category.Name);
        }


        [Fact]
        public async Task AllProductsInCategoryAsync_ShouldReturnAllProductFromCategory_OrderedByTitle()
        {
            // Arrange
            await this.db.Products.AddRangeAsync(products);
            var categoryService = new CategoryService(db, shoppingCart);

            var category = new Category
            {
                Id = 1,
                Name = "Snowboard",
                Products = products
            };

            // Act
            var result = await categoryService.AllProductsInCategoryAsync(category.Id); //return null but working in project

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Title == "A"
                         && r.ElementAt(1).Title == "B"
                         && r.ElementAt(2).Title == "C")
                .And
                .HaveCount(3);
        }
    }
}

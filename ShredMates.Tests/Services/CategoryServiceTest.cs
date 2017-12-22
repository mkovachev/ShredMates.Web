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
        private readonly Category category;

        public CategoryServiceTest()
        {
            TestStartup.GetMapper();
            this.db = TestStartup.GetDataBase();
            this.shoppingCart = TestStartup.GetShoppingCart();
            this.products = TestStartup.GetProducts();
            this.category = TestStartup.GetCategory();
        }

        [Fact]
        public async Task ByIdAsync_ShouldReturn_CorrectCategoryWithId()
        {
            // Arrange
            var categoryService = new CategoryService(db, shoppingCart);

            await this.db.AddAsync(category);
            await this.db.SaveChangesAsync();

            // Act
            var result = await categoryService.ByIdAsync(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);

            result.Should()
                .Match<Category>(r =>
                                    r.Id == category.Id
                                    && r.Name == category.Name);
        }

        [Fact]
        public async Task AllProductsInCategoryAsync_ShouldReturnAllProductFromCategory_OrderedByTitle()
        {
            // Arrange
            var categoryService = new CategoryService(db, shoppingCart);
            await this.db.Products.AddRangeAsync(products);
            await this.db.SaveChangesAsync();

            // Act
            var productsInCategory = await categoryService.AllProductsInCategoryAsync(category.Id);
            var result = productsInCategory.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);

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

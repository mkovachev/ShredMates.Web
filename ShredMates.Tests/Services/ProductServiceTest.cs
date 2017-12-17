using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Services
{
    public class ProductServiceTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;
        private readonly List<Product> products;
        private readonly Order order;

        public ProductServiceTest()
        {
            //TestStartup.GetMapper();
            this.db = TestStartup.GetDataBase();
            this.shoppingCart = TestStartup.GetShoppingCart();
            this.products = TestStartup.GetProducts();
            this.order = TestStartup.GetOrder();
        }

        [Fact]
        public async Task AllAsync_ShouldReturn_AllProducts()
        {
            // Arrange      
            var productService = new ProductService(db);

            // Act
            var result = await productService.AllAsync();

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task FindAsync_ShouldReturn_ProductById()
        {
            // Arrange
            await this.db.Products.AddRangeAsync(products);
            var productService = new ProductService(db);

            // Act
            var result = await productService.ByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should()
                .Match<Product>(p => p.Id == 1
                                    && p.Title == "A");
        }
    }
}

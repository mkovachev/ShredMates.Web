using AutoMapper;
using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using System.Collections.Generic;
using System.Linq;
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
            TestStartup.GetMapper();
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
            await this.db.Products.AddRangeAsync(products);
            await this.db.SaveChangesAsync();

            // Act
            var result = await productService.AllAsync();
            var item = result.ToList()[0];

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<ShredMates.Services.Models.ProductListingServiceModel>>(result);
        }

        [Fact]
        public async Task FindAsync_ShouldReturn_ProductById()
        {
            // Arrange
            var productService = new ProductService(db);
            await this.db.Products.AddRangeAsync(products);
            await this.db.SaveChangesAsync();

            // Act
            var result = await productService.ByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            result.Should()
                .Match<Product>(p => p.Id == 1
                                    && p.Title == "A");
        }
    }
}

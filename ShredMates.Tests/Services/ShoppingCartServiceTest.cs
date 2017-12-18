using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ShredMates.Tests.Services
{
    public class ShoppingCartServiceTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;
        private readonly List<AllProductServiceModel> products;
        private readonly Order order;

        public ShoppingCartServiceTest()
        {
            //TestStartup.GetMapper();
            this.db = TestStartup.GetDataBase();
            this.shoppingCart = TestStartup.GetShoppingCart();
            this.products = TestStartup.GetProducts();
            this.order = TestStartup.GetOrder();
        }

        [Fact]
        public async Task AllProductssAsync_ShouldReturn_AllProductsInCart()
        {
            // Arrrange
            var shoppingCartService = new ShoppingCartService(db, this.shoppingCart);

            var item = new ShoppingCartItem()
            {
                Id = 1,
                Product = products[0],
                Amount = 1,
                ShoppingCartId = "1"
            };
            this.shoppingCart.ShoppingCartItems.Add(item);
            await this.db.ShoppingCartItems.AddAsync(item);
            await this.db.SaveChangesAsync();

            // Act
            var items = await shoppingCartService.AllProductssAsync();

            // Assert
            items
                .Should()
                .Match(r => r.ElementAt(0).Product.Title == "A")
                .And
                .HaveCount(1);
        }

        [Fact]
        public async Task AddToCartAsync_ShoulAdd_ProductToCart()
        {
            // Arrrange
            var shoppingCartService = new ShoppingCartService(db, this.shoppingCart);

            var product = new AllProductServiceModel { Id = 73, Price = 1000, Title = "Play" };

            // Act
            await shoppingCartService.AddToCartAsync(product, 1);
            //await this.db.SaveChangesAsync();
            var result = await this.db.ShoppingCartItems.FirstOrDefaultAsync(p => p.Product.Title == "Play");

            // Arrange
            result.Should().NotBeNull();
            result
                .Should()
                .Match<ShoppingCartItem>(p => p.Product.Id == 73
                                    && p.Product.Title == "Play"
                                    && p.Product.Price == 1000);
        }
    }
}

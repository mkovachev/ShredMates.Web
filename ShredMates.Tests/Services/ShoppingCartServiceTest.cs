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
    public class ShoppingCartServiceTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;
        private readonly List<Product> products;
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
                Id = "1",
                Product = products[0],
                Amount = 1,
                ShoppingCartId = "1"
            };
            this.shoppingCart.ShoppingCartItems.Add(item);
            await this.db.SaveChangesAsync();

            // Act
            var items = shoppingCartService.AllProducts();

            // Assert
            items
                .Should()
                .Match(r => r.ElementAt(0).Product.Title == "A")
                .And
                .HaveCount(1);
        }

        [Fact]
        public void AddToCartAsync_ShoulAdd_ProductToCart()
        {
            // Arrrange
            var shoppingCartService = new ShoppingCartService(db, this.shoppingCart);

            var product = new Product { Id = 73, Price = 1000, Title = "Play" };

            // Act
            shoppingCartService.AddToCart(product, 1);

            var result = this.shoppingCart.ShoppingCartItems.FirstOrDefault(p => p.Product.Title == "Play");

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

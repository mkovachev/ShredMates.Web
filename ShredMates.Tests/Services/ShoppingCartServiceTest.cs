using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

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

            var id = 1;
            foreach (var product in products)
            {
                var item = new ShoppingCartItem()
                {
                    Id = id++,
                    Product = product,
                    Amount = 1,
                    ShoppingCartId = "1"
                };
                this.shoppingCart.ShoppingCartItems.Add(item);
                await this.db.ShoppingCartItems.AddAsync(item);
            }
            
            await this.db.SaveChangesAsync();

            // Act
            var items = await shoppingCartService.AllProductssAsync();

            // Assert
            items
                .Should()
                .Match(r => r.ElementAt(0).Product.Title == "A"
                         && r.ElementAt(1).Product.Title == "B"
                         && r.ElementAt(2).Product.Title == "C")
                .And
                .HaveCount(3);
        }
    }
}

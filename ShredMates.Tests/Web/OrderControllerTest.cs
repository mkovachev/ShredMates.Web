using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using ShredMates.Services.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Web
{
    public class OrderControllerTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;

        public OrderControllerTest()
        {
            this.db = TestStartup.CreateDatabase();
            this.shoppingCart = TestStartup.CreateShoppingCart();
        }

        [Fact]
        public async Task Checkout_ShouldCreate_Order()
        {
            // Arrange
            var orderService = new OrderService(db, shoppingCart);

            var order = new Order
            {
                Id = 2,
                OrderPlaced = DateTime.MinValue
            };

            // Act
            await orderService.CreateOrderAsync(order);

            var savedOrder = await this.db.Orders.FindAsync(order.Id);

            // Assert
            Assert.NotNull(savedOrder);
        }
    }
}

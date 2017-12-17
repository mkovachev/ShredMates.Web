using FluentAssertions;
using ShredMates.Data;
using ShredMates.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Services
{
    public class OrderServiceTest
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;
        private readonly List<Product> products;
        private readonly Order order;

        public OrderServiceTest()
        {
            //TestStartup.GetMapper();
            this.db = TestStartup.GetDataBase();
            this.shoppingCart = TestStartup.GetShoppingCart();
            this.products = TestStartup.GetProducts();
            this.order = TestStartup.GetOrder();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldSave_Order()
        {
            // Act
            var orders = await this.db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            var savedEntry = await this.db.Orders.FindAsync(order.OrderId);

            // Assert
            orders.Should().NotBeNull();
            savedEntry.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldSave_OrderDetails()
        {
            // Act    
            var orderDetail = new OrderDetail
            {
                Amount = 1,
                ProductId = 1,
                OrderId = 1,
                Price = 100
            };

            var orderDetails = await this.db.OrderDetails.AddAsync(orderDetail);
            await this.db.SaveChangesAsync();

            // Assert
            orderDetails.Should().NotBeNull();
        }
    }
}


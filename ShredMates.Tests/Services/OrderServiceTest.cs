using ShredMates.Data;
using ShredMates.Data.Models;
using System.Threading.Tasks;
using Xunit;

namespace ShredMates.Tests.Services
{
    public class OrderServiceTest
    {
        private readonly ShredMatesDbContext db;
        private readonly Order order;

        public OrderServiceTest()
        {
            this.db = TestStartup.GetDataBase();
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
            Assert.NotNull(orders);
            Assert.NotNull(savedEntry);
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
            Assert.NotNull(orderDetails);
        }
    }
}


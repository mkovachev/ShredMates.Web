using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using System;
using System.Threading.Tasks;

namespace ShredMates.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;

        public OrderService(ShredMatesDbContext db, ShoppingCart shoppingCart)
        {
            this.db = db;
            this.shoppingCart = shoppingCart;
        }

        public async Task CreateOrderAsync(Order order)
        {
            order.OrderPlaced = DateTime.UtcNow;

            await this.db.Orders.AddAsync(order);

            var shoppingCartItems = this.shoppingCart.ShoppingCartItems;

            foreach (var product in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = product.Amount,
                    ProductId = product.Product.Id,
                    OrderId = order.Id,
                    Price = product.Product.Price
                };

                await this.db.OrderDetails.AddAsync(orderDetail);

                await this.db.SaveChangesAsync();
            }

        }
    }
}

using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
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

        // public async Task CreateOrderAsync(Order order) { }
    }
}

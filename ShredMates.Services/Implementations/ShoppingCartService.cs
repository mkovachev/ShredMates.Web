﻿using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShredMates.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ShredMatesDbContext db;
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartService(ShredMatesDbContext db, ShoppingCart shoppingCart)
        {
            this.db = db;
            this.shoppingCart = shoppingCart;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
           // ISession session = services.GetRequiredService<HttpContextAccessor>()?.HttpContext.Session; // TODO
            //var db = services.GetService<ShredMatesDbContext>();
            //var cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
           // session.SetString("Id", cartId);
            var shoppingCart = new ShoppingCart() { Id = "Session" };

            return shoppingCart;
        }

        public async Task<List<ShoppingCartItem>> AllItemsAsync()
            => await this.db
                       .ShoppingCartItems
                       .Where(c => c.ShoppingCartId == shoppingCart.Id)
                       .Include(i => i.Product)
                       .ToListAsync();

        public async Task AddItemAsync(Product product, int amount)
        {
            var shoppingCartItem = await this.db
                                        .ShoppingCartItems
                                        .SingleOrDefaultAsync(s => s.Product.Id == product.Id
                                                              && s.ShoppingCartId == shoppingCart.Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCart.Id,
                    Product = product,
                    Amount = 1
                };

                this.db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            await this.db.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Product product)
        {
            var shoppingCartItem = await this.db
                                    .ShoppingCartItems
                                    .SingleOrDefaultAsync(s => s.Product.Id == product.Id
                                            && s.ShoppingCartId == shoppingCart.Id);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    this.db.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            await this.db.SaveChangesAsync();
        }

        public async Task ClearCartAsync()
        {
            var cartItems = await this.db
                                .ShoppingCartItems
                                .Where(c => c.ShoppingCartId == shoppingCart.Id)
                                .ToListAsync();

            this.db.ShoppingCartItems.RemoveRange(cartItems);

            await this.db.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalAsync()
            => await this.db
                       .ShoppingCartItems
                       .Where(c => c.ShoppingCartId == shoppingCart.Id)
                       .Select(c => c.Product.Price * c.Amount)
                       .SumAsync();

        public async Task<ShoppingCartItem> FindItemByIdAsync(int productId)
            => await this.db.ShoppingCartItems.FirstOrDefaultAsync(s => s.Product.Id == productId);

        public async Task CreateOrderAsync(Order order)
        {
            order.OrderPlaced = DateTime.UtcNow;

            this.db.Orders.Add(order);

            var shoppingCartItems = shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.Id,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Product.Price
                };

                this.db.OrderDetails.Add(orderDetail);
            }

            await this.db.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string id)
        {
            var cartItems = this.db
                             .ShoppingCartItems
                             .Where(cart => cart.ShoppingCartId == id);

            this.db.ShoppingCartItems.RemoveRange(cartItems);

            await this.db.SaveChangesAsync();
        }
    }
}
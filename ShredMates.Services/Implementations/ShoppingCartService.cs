using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
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

        public async Task<Product> FindProductByIdAsync(int productId)
            => await this.db.Products.FirstOrDefaultAsync(s => s.Id == productId);

        public List<ShoppingCartItem> AllProducts()
            => this.shoppingCart
                       .ShoppingCartItems
                       .Where(c => c.ShoppingCartId == shoppingCart.Id)
                       .ToList();

        public void AddToCart(Product product, int amount)
        {
            var shoppingCartItem = this.shoppingCart
                                        .ShoppingCartItems
                                        .Where(c => c.ShoppingCartId == shoppingCart.Id
                                                        && c.Product.Id == product.Id)
                                        .FirstOrDefault();
                   
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Id = Guid.NewGuid().ToString(),
                    ShoppingCartId = shoppingCart.Id,
                    Product = product,
                    Amount = 1
                };

                this.shoppingCart.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            return;
        }

        public void RemoveProduct(Product product)
        {
            var shoppingCartItem = this.shoppingCart
                                        .ShoppingCartItems
                                        .Where(s => s.Product.Id == product.Id
                                                   && s.ShoppingCartId == shoppingCart.Id)
                                         .FirstOrDefault();

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    this.shoppingCart.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
        }

        public void ClearCart()
        {
            this.shoppingCart.ShoppingCartItems.Clear();
        }

        public decimal GetTotal()
            =>  this.shoppingCart
                       .ShoppingCartItems
                       .Where(c => c.ShoppingCartId == shoppingCart.Id)
                       .Select(c => c.Product.Price * c.Amount)
                       .Sum();
    }
}

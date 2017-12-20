﻿using ShredMates.Data.Models;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<Product> FindProductByIdAsync(int productId);

        void AddToCart(Product product, int amount);

        void RemoveProduct(Product product);

        void ClearCart();

        decimal GetTotal();

        List<ShoppingCartItem> AllProducts();
    }
}

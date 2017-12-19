using ShredMates.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<Product> FindProductByIdAsync(int productId);

        Task AddToCartAsync(Product product, int amount);

        Task RemoveProductAsync(Product product);

        Task ClearCartAsync();

        Task<decimal> GetTotalAsync();

        Task<List<ShoppingCartItem>> AllProductsAsync();

        Task CreateOrderAsync(Order order);
    }
}

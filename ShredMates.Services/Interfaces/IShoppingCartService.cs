using ShredMates.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartItem> FindItemByIdAsync(int productId);

        Task AddItemAsync(Product product, int amount);

        Task RemoveItemAsync(Product product);

        Task ClearCartAsync();

        Task<decimal> GetTotalAsync();

        Task<List<ShoppingCartItem>> AllItemsAsync();

        Task CreateOrderAsync(Order order);

        Task ClearCartAsync(string id);
    }
}

using ShredMates.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<AllProductServiceModel> FindProductByIdAsync(int productId);

        Task AddToCartAsync(AllProductServiceModel product, int amount);

        Task RemoveProductAsync(AllProductServiceModel product);

        Task ClearCartAsync();

        Task<decimal> GetTotalAsync();

        Task<List<ShoppingCartItem>> AllProductssAsync();

        Task CreateOrderAsync(Order order);

        Task ClearCartAsync(string id);
    }
}

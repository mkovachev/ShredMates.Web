using ShredMates.Data.Models;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
    }
}

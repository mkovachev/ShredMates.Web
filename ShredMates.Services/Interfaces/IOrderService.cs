using ShredMates.Data.Models;
using ShredMates.Services.Common;
using System.Threading.Tasks;

namespace ShredMates.Services.Interfaces
{
    public interface IOrderService : ITransientService
    {
        Task CreateOrderAsync(Order order);
    }
}

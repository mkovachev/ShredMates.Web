using ShredMates.Common.Mapping;
using ShredMates.Data.Models;

namespace ShredMates.Services.Models
{
    public class ShoppingCartServiceModel: IMapFrom<ShoppingCart>
    {
        public string Id { get; set; }
    }
}

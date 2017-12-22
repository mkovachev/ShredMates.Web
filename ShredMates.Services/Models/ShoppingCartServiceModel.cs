using ShredMates.Common.Mapping;

namespace ShredMates.Services.Models
{
    public class ShoppingCartServiceModel: IMapFrom<ShoppingCart>
    {
        public string Id { get; set; }
    }
}

using System.Collections.Generic;

namespace ShredMates.Data.Models
{
    public partial class ProductAttribute
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AttributeParam> AttributeParams { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}

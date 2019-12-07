using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Interfaces;
using ShredMates.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShredMates.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ShredMatesDbContext db;
        private readonly IMapper mapper;

        public ProductService(ShredMatesDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductListingServiceModel>> AllAsync(int page = 1, int pageSize = DataConstants.PageSize)
        {
            return await this.db
                        .Products
                        .OrderByDescending(p => p.DateCreated)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ProjectTo<ProductListingServiceModel>(this.mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<Product> ByIdAsync(int id) => await this.db.Products.FindAsync(id);

        public async Task<List<ProductListingServiceModel>> SearchAsync(string search)
        {
            search = search ?? string.Empty;

            return await this.db
                        .Products
                        .OrderByDescending(p => p.DateCreated)
                        .Where(p => p.Title.ToLower().Contains(search.ToLower()))
                        .ProjectTo<ProductListingServiceModel>(this.mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public int TotalPages() => this.db.Products.Count();
    }
}

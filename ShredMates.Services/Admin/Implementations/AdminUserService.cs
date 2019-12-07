using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly ShredMatesDbContext db;
        private readonly IMapper mapper;
        public AdminUserService(ShredMatesDbContext db, IMapper mapper)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AdminUserServiceModel>> AllAsync()
            => await this.db
                       .Users
                       .ProjectTo<AdminUserServiceModel>(this.mapper.ConfigurationProvider)
                       .ToListAsync();
    }
}

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

        public AdminUserService(ShredMatesDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserServiceModel>> AllAsync()
            => await this.db
                       .Users
                       .ProjectTo<AdminUserServiceModel>(null)
                       .ToListAsync();
    }
}

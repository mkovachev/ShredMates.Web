using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShredMates.Data;
using ShredMates.Services.Admin.Interfaces;
using ShredMates.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShredMates.Services.Admin.Implementations
{
    public class UserService : IUserService
    {
        private readonly ShredMatesDbContext db;

        public UserService(ShredMatesDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<UserServiceModel>> AllAsync()
            => await this.db
                       .Users
                       .ProjectTo<UserServiceModel>()
                       .ToListAsync();
    }
}

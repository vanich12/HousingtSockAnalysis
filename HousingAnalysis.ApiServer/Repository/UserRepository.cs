using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HousingAnalysis.ApiServer.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly HouseDbContext _context;
        private readonly DbSet<User?> _dbSet;

        public async Task<User?> GetByEmail(string email)
        {
            return await this._dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public UserRepository(HouseDbContext context) : base(context)
        {
            this._context = context;
            this._dbSet = context.Users;
        }
    }
}
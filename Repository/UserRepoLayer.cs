using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class UserRepoLayer : IUserRepoLayer
    {
        private readonly DataContext _dbContext;

        public UserRepoLayer(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUser> getUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            return user;
        }

        public async Task<IEnumerable<AppUser>> getUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            return users;
        }
    }
}
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

        public async Task<AppUser> GetUserByUsername(string username)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.UserName == username);

            return user;
        }

        public async Task<IEnumerable<AppUser>> getUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            return users;
        }

        public async Task<AppUser> Register(AppUser user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
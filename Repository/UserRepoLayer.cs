using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class UserRepoLayer : IUserRepoLayer
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepoLayer(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _dbContext.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _dbContext.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> getUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            return user;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _dbContext.Users
                .Include(p=> p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);

            return user;
        }

        public async Task<IEnumerable<AppUser>> getUsersAsync()
        {
            var users = await _dbContext.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<AppUser> RegisterAsync(AppUser user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repository;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepoLayer _userRepoLayer;

        public UserLogic(IUserRepoLayer userRepoLayer)
        {
            _userRepoLayer = userRepoLayer;
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            IEnumerable<AppUser> users = await _userRepoLayer.getUsers();

            return users;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            AppUser user = await _userRepoLayer.getUserById(id);

            return user;
        }
    }
}

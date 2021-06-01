using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Models;
using Repository;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepoLayer _userRepoLayer;
        private readonly ITokenService _tokenService;

        public UserLogic(IUserRepoLayer userRepoLayer, ITokenService tokenService)
        {
            _userRepoLayer = userRepoLayer;
            _tokenService = tokenService;
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

        public async Task<UserDto> Register(string username, string password)
        {
            AppUser user = UserMapper.GetNewUserWithHashedPassword(password);
            user.UserName = username;
            AppUser newUser = await _userRepoLayer.Register(user);
            UserDto regUser = new UserDto
            {
                Username = newUser.UserName,
                Token = _tokenService.CreateToken(newUser)
            };

            return regUser;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userRepoLayer.UserExists(username);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userRepoLayer.GetUserByUsername(username);
        }

        public bool Login(AppUser user, string password)
        {
            bool isLogin = UserMapper.CheckLoginPassword(user, password);

            return isLogin;
        }
    }
}

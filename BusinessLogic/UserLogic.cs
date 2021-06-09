using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepoLayer _userRepoLayer;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserLogic(IUserRepoLayer userRepoLayer, ITokenService tokenService, IMapper mapper)
        {
            _userRepoLayer = userRepoLayer;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            IEnumerable<MemberDto> users = await _userRepoLayer.GetMembersAsync();

            return users;
        }

        
        public async Task<AppUser> GetUserById(int id)
        {
            AppUser user = await _userRepoLayer.getUserByIdAsync(id);

            return user;
        }

        public async Task<UserDto> Register(string username, string password)
        {
            AppUser user = UserMapper.GetNewUserWithHashedPassword(password);
            user.UserName = username;
            AppUser newUser = await _userRepoLayer.RegisterAsync(user);
            UserDto regUser = new UserDto
            {
                Username = newUser.UserName,
                Token = _tokenService.CreateToken(newUser)
            };

            return regUser;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userRepoLayer.UserExistsAsync(username);
        }

        public async Task<MemberDto> GetUserByUsername(string username)
        {
            var user = await _userRepoLayer.GetMemberAsync(username);
            return _mapper.Map<MemberDto>(user);
        }

        public async Task<AppUser> GetUserForLogin(string username)
        {
            return await _userRepoLayer.GetUserByUsernameAsync(username);
        }

        public bool Login(AppUser user, string password)
        {
            bool isLogin = UserMapper.CheckLoginPassword(user, password);

            return isLogin;
        }

        public async Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto, string username)
        {
            var user = await GetUserForLogin(username);
            _mapper.Map(memberUpdateDto, user);

            _userRepoLayer.Update(user);

            if(await _userRepoLayer.SaveAllAsync()) return true;

            return false;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _userRepoLayer.SaveAllAsync();
        }
    }
}

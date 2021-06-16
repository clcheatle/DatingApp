using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using API.Models;
using API.Repository;
using AutoMapper;

namespace API.BusinessLogic
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

        public async Task<PagedList<MemberDto>> GetUsers(UserParams userParams)
        {
            var users = await _userRepoLayer.GetMembersAsync(userParams);

            return users;
        }

        
        public async Task<AppUser> GetUserById(int id)
        {
            AppUser user = await _userRepoLayer.getUserByIdAsync(id);

            return user;
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

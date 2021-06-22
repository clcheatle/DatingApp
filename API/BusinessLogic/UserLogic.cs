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
    public class UserLogic
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserLogic(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<PagedList<MemberDto>> GetUsers(UserParams userParams)
        {
            var users = await _unitOfWork.UserRepository.GetMembersAsync(userParams);

            return users;
        }


        public async Task<AppUser> GetUserById(int id)
        {
            AppUser user = await _unitOfWork.UserRepository.getUserByIdAsync(id);

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _unitOfWork.UserRepository.UserExistsAsync(username);
        }

        public async Task<MemberDto> GetUserByUsername(string username, bool isCurrentUser)
        {
            var user = await _unitOfWork.UserRepository.GetMemberAsync(username, isCurrentUser);
            return _mapper.Map<MemberDto>(user);
        }

        public async Task<AppUser> GetUserForLogin(string username)
        {
            return await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        }

        public async Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto, string username)
        {
            var user = await GetUserForLogin(username);
            _mapper.Map(memberUpdateDto, user);

            _unitOfWork.UserRepository.Update(user);

            if (await _unitOfWork.Complete()) return true;

            return false;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _unitOfWork.Complete();
        }
    }
}

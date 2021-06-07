using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserLogic _userLogic;
        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        
        [HttpGet]
        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            var users = await _userLogic.GetUsers();

            return users;
        }

        [HttpGet("{username}")]
        public async Task<MemberDto> GetUserByUsername(string username)
        {
            var users = await _userLogic.GetUserByUsername(username);

            return users;
        }
    }
}
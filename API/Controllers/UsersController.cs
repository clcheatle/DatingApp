using System.Collections.Generic;
using System.Security.Claims;
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var updateSuccess = await _userLogic.UpdateUser(memberUpdateDto, username);
            if(updateSuccess) return NoContent();

            return BadRequest("Failed to update the user");
        } 
    }
}
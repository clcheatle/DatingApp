using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            var users = await _userLogic.GetUsers();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<AppUser> GetUserById(int id)
        {
            var users = await _userLogic.GetUserById(id);

            return users;
        }
    }
}
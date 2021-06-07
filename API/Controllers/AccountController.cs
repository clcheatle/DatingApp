using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserLogic _userLogic;
        private readonly ITokenService _tokenService;
        public AccountController(IUserLogic userLogic, ITokenService tokenService)
        {
            _userLogic = userLogic;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await _userLogic.UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");

            }
            UserDto newUser = await _userLogic.Register(registerDto.Username.ToLower(), registerDto.Password);

            return newUser;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userLogic.GetUserForLogin(loginDto.Username);

            if(user == null) return Unauthorized("Invalid username");

            if(_userLogic.Login(user, loginDto.Password) == false) return Unauthorized("Invalid password");

            UserDto logUser = new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

            return logUser;
        }
    }
}
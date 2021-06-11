using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserLogic _userLogic;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(IUserLogic userLogic, ITokenService tokenService, IMapper mapper)
        {
            _userLogic = userLogic;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await _userLogic.UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");

            }

            var user = _mapper.Map<AppUser>(registerDto);

            UserDto newUser = await _userLogic.Register(user, registerDto);

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
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };

            return logUser;
        }
    }
}
using System.Threading.Tasks;
using API.Helpers;
using API.Models;

namespace API.BusinessLogic
{
    public interface IUserLogic
    {
         /// <summary>
         /// Gets a list of users from the repo layer
         /// </summary>
         /// <returns></returns>
         public Task<PagedList<MemberDto>> GetUsers(UserParams userParams);

         /// <summary>
         /// Get a user by id from the repo layer
         /// </summary>
         /// <param name="i"></param>
         /// <returns></returns>
         public Task<AppUser> GetUserById(int id);

        /// <summary>
        /// Given a username and password, register a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
         public Task<UserDto> Register(AppUser user, RegisterDto registerDto);

        /// <summary>
        /// Check to see if the user name already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
         public Task<bool> UserExists(string username);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
         public Task<MemberDto> GetUserByUsername(string username);
         public Task<AppUser> GetUserForLogin(string username);

        /// <summary>
        /// Method to check password to login in user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         public bool Login(AppUser user, string password);

         public Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto, string username);

         public Task<bool> SaveAllAsync();


    }
}
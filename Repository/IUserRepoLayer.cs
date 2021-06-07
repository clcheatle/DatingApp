using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public interface IUserRepoLayer
    {
        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns></returns>
         public Task<IEnumerable<AppUser>> getUsersAsync();

         /// <summary>
         /// Get a user by id
         /// </summary>
         /// <param name="i"></param>
         /// <returns></returns>
         public Task<AppUser> getUserByIdAsync(int id);

        /// <summary>
        /// Add a new App User to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         public Task<AppUser> RegisterAsync(AppUser user);

        /// <summary>
        /// Method to check if a user with the given username already exists in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
         public Task<bool> UserExistsAsync(string username);

        /// <summary>
        /// Return a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
         public Task<AppUser> GetUserByUsernameAsync(string username);

         public void Update(AppUser user);

         Task<bool> SaveAllAsync();

         Task<IEnumerable<MemberDto>> GetMembersAsync();
         Task<MemberDto> GetMemberAsync(string username);
    }
}
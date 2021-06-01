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
         public Task<IEnumerable<AppUser>> getUsers();

         /// <summary>
         /// Get a user by id
         /// </summary>
         /// <param name="i"></param>
         /// <returns></returns>
         public Task<AppUser> getUserById(int id);

        /// <summary>
        /// Add a new App User to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         public Task<AppUser> Register(AppUser user);

        /// <summary>
        /// Method to check if a user with the given username already exists in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
         public Task<bool> UserExists(string username);

        /// <summary>
        /// Return a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
         public Task<AppUser> GetUserByUsername(string username);
    }
}
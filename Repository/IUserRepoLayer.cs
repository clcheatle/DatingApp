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
    }
}
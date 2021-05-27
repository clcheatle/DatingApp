using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BusinessLogic
{
    public interface IUserLogic
    {
         /// <summary>
         /// Gets a list of users from the repo layer
         /// </summary>
         /// <returns></returns>
         public Task<IEnumerable<AppUser>> GetUsers();

         /// <summary>
         /// Get a user by id from the repo layer
         /// </summary>
         /// <param name="i"></param>
         /// <returns></returns>
         public Task<AppUser> GetUserById(int id);
    }
}
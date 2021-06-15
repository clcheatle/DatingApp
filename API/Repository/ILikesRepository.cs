using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;

namespace API.Repository
{
    public interface ILikesRepository
    {
         Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
         Task<AppUser> GetUserWithLikes(int userId);
         Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}
using System.Threading.Tasks;
using API.Repository;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
         IUserRepoLayer UserRepository {get;}
         IMessageRepository MessageRepository {get;}
         ILikesRepository LikesRepository {get;}
         Task<bool> Complete();
         bool HasChanges();

    }
}
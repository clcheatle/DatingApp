using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repository
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
         Task<Photo> GetPhotoById(int id);
         void RemovePhoto(Photo photo);
    }
}
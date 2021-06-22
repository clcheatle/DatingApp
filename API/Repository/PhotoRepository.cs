using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _dbcontext;
        public PhotoRepository(DataContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            return await _dbcontext.Photos
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
        {
            return await _dbcontext.Photos
                .IgnoreQueryFilters()
                .Where(p => p.isApproved == false)
                .Select(u => new PhotoForApprovalDto
                {
                    Id = u.Id,
                    Username = u.AppUser.UserName,
                    Url = u.Url,
                    isApproved = u.isApproved
                })
                .ToListAsync();
        }

        public void RemovePhoto(Photo photo)
        {
            _dbcontext.Photos.Remove(photo);
        }
    }
}
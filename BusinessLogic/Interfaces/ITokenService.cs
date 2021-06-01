using Models;

namespace BusinessLogic.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser User); 
    }
}
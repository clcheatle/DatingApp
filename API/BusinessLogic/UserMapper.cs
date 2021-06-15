using System.Security.Cryptography;
using System.Text;
using API.Models;

namespace API.BusinessLogic
{
    public class UserMapper
    {
        public static AppUser GetNewUserWithHashedPassword(string password)
        {
            using(var hmac = new HMACSHA512())
            {
                AppUser user = new AppUser()
                {
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };

                return user;
            }
        }

        public static bool CheckLoginPassword(AppUser user, string password)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i])
                    return false;
            }

            return true;
        }
    }
}
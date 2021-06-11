using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var username = user.FindFirst(ClaimTypes.Name)?.Value;
            return username;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return id;
        }
    }
}
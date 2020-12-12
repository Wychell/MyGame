using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace MyGame.Infra.Security
{
    public class AuthUserResolver : IAuthUserResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthUserResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var id = user?.Claims?.FirstOrDefault(x => x.Type == "id")?.Value;
            return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
        }
    }
}

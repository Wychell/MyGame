using Microsoft.AspNetCore.Authorization;

namespace MyGame.Infra.Security
{
    public static class MyGamePolicy
    {
        public static AuthorizationPolicy policy =
                new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();
    }
}

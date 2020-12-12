using System;

namespace MyGame.Infra.Security
{
    public interface IAuthUserResolver
    {
        Guid GetUserId();

    }
}

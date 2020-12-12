using Microsoft.Extensions.Configuration;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Infra.Security
{
    public interface IJwtTokenProvider
    {
        IConfiguration Configuration { get; }
        string GenerateToken(User user, int expireTime = 24);
    }
}

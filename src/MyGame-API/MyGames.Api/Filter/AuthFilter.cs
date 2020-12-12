using Microsoft.AspNetCore.Mvc.Authorization;
using MyGame.Infra.Security;

namespace MyGames.Api.Filter
{
    public class AuthFilter : AuthorizeFilter
    {
        public AuthFilter() : base(MyGamePolicy.policy)
        {
            /*Filtro de Autenticação responsavel por todos os controlles, já default na aplicação*/
        }
    }
}

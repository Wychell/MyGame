using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MyGames.Api.Filter
{
    public class MyGameExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            HttpResponse response = context.HttpContext.Response;

            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new JsonResult("Ocorreu um erro ao processar a requisição");
        }
    }
}

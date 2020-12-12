using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGames.Api.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyGames.Api.Filter
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext notificationContext;
        public NotificationFilter(INotificationContext notificationContext)
        {
            this.notificationContext = notificationContext;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {

            if (notificationContext.HasNotifications )
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";
                var resultModel = new ModelResult<string> { Messages = notificationContext.Notifications.ToArray(), Success = !notificationContext.HasNotifications };
                var notifications = JsonConvert.SerializeObject(resultModel, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}

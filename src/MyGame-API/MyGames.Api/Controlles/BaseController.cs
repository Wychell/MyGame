using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using MyGames.Api.ViewModel;

namespace MyGames.Api.Controlles
{
    public class BaseController : ControllerBase
    {
        protected ModelResult<T> MakeResult<T>(bool success, T data, Notification[] notifications = null)
        {
            return new ModelResult<T>()
            {
                Data = data,
                Success = success,
                Messages = notifications
            };
        }
        protected ModelResult<T> MakeResult<T>(T data, Notification[] notifications = null)
        {
            return new ModelResult<T>()
            {
                Data = data,
                Success = data != null,
                Messages = notifications
            };
        }
    }
}

using Flunt.Notifications;

namespace MyGames.Api.ViewModel
{
    public class ModelResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public Notification[] Messages { get; set; }
    }
}

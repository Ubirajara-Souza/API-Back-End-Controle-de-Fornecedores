using Bira.App.Providers.Service.Interfaces;

namespace Bira.App.Providers.Service.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notification;

        public Notifier()
        {
            _notification = new List<Notification>();
        }
        public void Handle(Notification notification)
        {
            _notification.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notification;
        }

        public bool HasNotification()
        {
            return _notification.Any();
        }
    }
}

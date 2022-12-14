using Bira.App.Providers.Service.Notifications;

namespace Bira.App.Providers.Service.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}

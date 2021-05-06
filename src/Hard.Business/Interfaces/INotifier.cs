using System.Collections.Generic;

namespace Hard.Business.Interfaces
{
    public interface INotifier 
    {
        public bool HasNotification();

        public List<INotification> GetNotifications();

        public void Handle(INotification notification);
    }
}

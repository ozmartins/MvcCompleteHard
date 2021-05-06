using Hard.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hard.Business.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<INotification> _notifications;

        public Notifier()
        {
            _notifications = new List<INotification>();
        }

        public List<INotification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(INotification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}

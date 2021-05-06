using Hard.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Business.Notifications
{
    public class Notification : INotification
    {
        public string Message { get; }

        public Notification(string message)
        {
            Message = message;
        }               
    }
}

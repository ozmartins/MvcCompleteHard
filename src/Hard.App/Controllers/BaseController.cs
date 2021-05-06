using Hard.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hard.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        public bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}

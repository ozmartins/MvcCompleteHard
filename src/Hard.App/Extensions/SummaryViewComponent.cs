using Hard.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var notifications = await Task.FromResult(_notifier.GetNotifications());

            notifications.ForEach(n => ViewData.ModelState.AddModelError(String.Empty, n.Message));

            return View();
        }
    }
}

using FluentValidation;
using FluentValidation.Results;
using Hard.Business.Interfaces;
using Hard.Business.Models;
using Hard.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ExecuteValidation<TValidator, TEntity>(TValidator validator, TEntity entity) where TValidator : AbstractValidator<TEntity> where TEntity : Entity
        {
            var result = validator.Validate(entity);

            if (result.IsValid) return true;

            Notify(result);

            return false;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notify(item.ErrorMessage);
            }
        }
        protected void Notify(string erroMessage)
        {
            _notifier.Handle(new Notification(erroMessage));
        }
    }
}

using FluentValidation;
using FluentValidation.Results;
using Hard.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Business.Services
{
    public abstract class BaseService
    {
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
            throw new Exception(erroMessage);
        }
    }
}

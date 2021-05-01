using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Business.Models.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(2, 200);

            RuleFor(p => p.Description).NotEmpty().Length(2, 1000);

            RuleFor(p => p.Price).GreaterThan(0);
        }
    }
}

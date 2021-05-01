using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Business.Models.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Street).NotEmpty().Length(2, 200);

            RuleFor(a => a.Neighborhood).NotEmpty().Length(2, 100);

            RuleFor(a => a.ZipCode).NotEmpty().Length(8, 8);

            RuleFor(a => a.CityName).NotEmpty().Length(2, 100);

            RuleFor(a => a.State).NotEmpty().Length(2, 50);

            RuleFor(a => a.Number).NotEmpty().Length(2, 50);
        }
    }
}

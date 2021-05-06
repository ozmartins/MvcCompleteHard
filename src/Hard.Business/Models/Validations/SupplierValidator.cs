using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Business.Models.Validations
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("The field {PropertyName} can't be empty")
                .Length(2, 200).WithMessage("The field {PropertyName} has to have {MinLength} and {MaxLength} character.");

            When(s => s.DocumentType == DocumentType.CPF, () => {
                RuleFor(s => s.DocumentId.Length).Equal(CpfValidator.CpfLength).WithMessage("CPF size is inválid.");
                RuleFor(s => CpfValidator.Validate(s.DocumentId)).Equal(true).WithMessage("CPF {PropertyValue} is inválid");
            });

            When(s => s.DocumentType == DocumentType.CNPJ, () => {
                RuleFor(s => s.DocumentId.Length).Equal(CnpjValidator.CnpjLength).WithMessage("CNPJ size is inválid.");
                RuleFor(s => CnpjValidator.Validate(s.DocumentId)).Equal(true).WithMessage("CNPJ {PropertyValue} is inválid");
            });
        }
    }
}

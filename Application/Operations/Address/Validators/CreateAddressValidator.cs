using Application.Operations.Address.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Validators
{
    public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressValidator()
        {
           
            
                RuleFor(x => x.AddressTitle)
                    .NotEmpty().WithMessage("Adres başlığı zorunludur.")
                    .MaximumLength(100).WithMessage("Adres başlığı en fazla 100 karakter olabilir.");

                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Adres zorunludur.")
                    .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir.");

                RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("Kullanıcı id zorunludur.");
            


        }
    }
}

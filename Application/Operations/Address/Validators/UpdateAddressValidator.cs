using Application.Operations.Address.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Address.Validators
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
    {

        public UpdateAddressValidator()
        {
            RuleFor(x => x.AddressTitle).NotEmpty().WithMessage("Adres başlığı boş olamaz");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz");
     
        }

    }
}

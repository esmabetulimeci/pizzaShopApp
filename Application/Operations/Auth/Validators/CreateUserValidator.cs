using Application.Operations.Auth.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Auth.Validators
{
    public class CreateUserValidator : AbstractValidator<RegisterCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad alanı boş geçilemez");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez");
        }

    }

}

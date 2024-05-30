using Application.Operations.Order.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Order.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {

        public CreateOrderValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı id boş olamaz");
            RuleFor(x => x.AddressId).NotEmpty().WithMessage("Adres id boş olamaz");
            RuleFor(x => x.ProductIds).NotEmpty().WithMessage("Sipariş ürünleri boş olamaz");
            RuleFor(x => x.ProductIds).Must(x => x.Count > 0).WithMessage("Sipariş ürünleri boş olamaz");
        }
    }
}

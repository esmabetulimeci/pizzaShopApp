using Application.Operations.Product.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Product.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            //to turkish

            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş olamaz");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Ürün adı 100 karakterden fazla olamaz");

        }
    }
}

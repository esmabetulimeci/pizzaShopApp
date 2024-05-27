using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Product.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public DeleteProductCommand(int ıd)
        {
            Id = ıd;
        }

        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteProductCommand, int>
        {
            private IMediator _mediator;
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _dbContext.Products.FindAsync(request.Id);
                if (product == null)
                {
                    throw new Exception("PRODUCT_NOT_FOUND");
                }

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return product.Id;
            }
        }


    }
}

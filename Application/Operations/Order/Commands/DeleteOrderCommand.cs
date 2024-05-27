using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Order.Commands
{
    public class DeleteOrderCommand : IRequest<int>
    {

        public int OrderId { get; set; }

        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public class Handler : IRequestHandler<DeleteOrderCommand, int>
        {
            private IMediator _mediator;
            private readonly IPizzaShopAppDbContext _dbContext;

            public Handler(IPizzaShopAppDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _dbContext.Orders.FindAsync(request.OrderId);
                if (order == null)
                {
                    throw new Exception("ORDER_NOT_FOUND");
                }

                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return order.Id;
            }
        }






    }
}

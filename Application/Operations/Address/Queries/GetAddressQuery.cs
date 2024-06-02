using Application.Common.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAddressQuery : IRequest<IEnumerable<AddressAggregate>>
{
    public string SearchKeyword { get; set; }
    public int UserId { get; set; } // Kullanıcı kimliği alanı eklendi
    public string Title { get; set; } // Başlık alanı eklendi
    public string Address { get; set; } // Adres alanı eklendi

    public class Handler : IRequestHandler<GetAddressQuery, IEnumerable<AddressAggregate>>
    {
        private readonly IPizzaShopAppDbContext _dbContext;

        public Handler(IPizzaShopAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AddressAggregate>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            // Eğer kullanıcı kimliği, başlık veya adres alanları boş ise hata fırlat
            if (request.UserId == 0 || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Address))
            {
                throw new Exception("INVALID_REQUEST");
            }


            IQueryable<AddressAggregate> query = _dbContext.Addresses;

            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                query = query.Where(a => EF.Functions.Like(a.Address, $"%{request.SearchKeyword}%"));
            }

            var addresses = await query.ToListAsync();

            if (addresses == null || !addresses.Any())
            {
                throw new Exception("NO_ADDRESSES_FOUND");
            }

            return addresses;
        }
    }
}

using Customer.BusinessLogic.Database.Entities;
using Customer.DatabaseLogic;
using Customer.DatabaseLogic.Repositories;

namespace Customer.BusinessLogic.Database.Repositories
{
    public class CountryRepository(AppDbContext appDbContext) 
        : BaseRepository<CountryEntity>(appDbContext)
    {
    }
}

using Customer.DatabaseLogic;
using Customer.DatabaseLogic.Repositories;

namespace Customer.BusinessLogic.Database.Repositories
{
    public class CustomerRepository(AppDbContext appDbContext) 
        : BaseRepository<Entities.Customer>(appDbContext)
    {
    }
}

using Customer.BusinessLogic.Database.Repositories;
using Customer.BusinessLogic.Models;
using Microsoft.Extensions.Logging;

namespace Customer.BusinessLogic.Services.Country
{
    public class CountryService
    {
        private readonly CountryRepository _repository;
        private readonly ILogger<CountryService> _logger;

        public CountryService(CountryRepository repository, ILogger<CountryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<CountryModel>> GetCountries()
        {
            var countries = await _repository.GetAllAsync();
            return countries.Select(c => c.ToCountryModel());
        }

    }
}

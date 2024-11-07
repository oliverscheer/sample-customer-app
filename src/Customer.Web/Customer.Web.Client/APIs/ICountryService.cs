using Customer.BusinessLogic.Models;
using Refit;

namespace Customer.Web.Client.APIs
{
    public interface ICountryService
    {
        [Get("/api/country")]
        Task<Result<CountryModel>> GetAllCountries();
    }
}

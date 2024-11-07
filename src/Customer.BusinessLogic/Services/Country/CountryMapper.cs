using Customer.BusinessLogic.Database.Entities;
using Customer.BusinessLogic.Models;

namespace Customer.BusinessLogic.Services.Country
{
    public static class CountryMapper
    {
        public static CountryModel ToCountryModel(this CountryEntity countryEntity)
        {
            return new CountryModel(
                countryEntity.Id, 
                countryEntity.Name,
                countryEntity.Code);
        }
    }
}


using System.Collections.Generic;

namespace OnLooker.Core.Services
{
    public class CCountryService
    {
        private readonly IGateway<CountryInfo> dbService;
        public CCountryService(IGateway<CountryInfo> gateway)
        {
            this.dbService = gateway;
        }

        public List<CountryInfo> GetAllCountries( )
        {
            return dbService.GetAll();
        }
    }
}

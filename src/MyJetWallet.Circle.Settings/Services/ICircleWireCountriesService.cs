using MyJetWallet.Circle.Settings.Domain;
using System.Collections.Generic;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface ICircleWireCountriesService
    {
        IReadOnlyCollection<BankAccountCountry> GetAllSupportedCountries();
    }
}
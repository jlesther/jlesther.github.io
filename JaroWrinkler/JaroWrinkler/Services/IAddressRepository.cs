using JaroWrinklerSimilarity.Models;

namespace JaroWrinklerSimilarity.Services
{
    public interface IAddressRepository
    {
        Task<IEnumerable<CustomerAddress>> GetAllAddressesAsync();
    }
}
using JaroWrinklerSimilarity.Models;
using JaroWrinklerSimilarity.Models.Responses;

namespace JaroWrinklerSimilarity.Services
{
    public interface IAddressMatchingService
    {
        /// <summary>
        /// Matches the input address against stored addresses and returns the best match.
        /// </summary>
        /// <param name="input">The address input from the client.</param>
        /// <returns>A structured address match response.</returns>
        Task<AddressMatchResponse> MatchAddressAsync(AddressInput input);
    }
}
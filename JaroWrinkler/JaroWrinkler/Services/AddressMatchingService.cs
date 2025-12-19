using JaroWrinklerSimilarity.Models;
using JaroWrinklerSimilarity.Models.Enums;
using JaroWrinklerSimilarity.Models.Responses;

namespace JaroWrinklerSimilarity.Services
{
    public class AddressMatchingService : IAddressMatchingService
    {
        private readonly ISimilarityService _similarityService;
        private readonly INormalizerService _normalizerService;
        private readonly IAddressRepository _repository;

        private const double EXACT_THRESHOLD = 0.98;
        private const double SIMILAR_THRESHOLD = 0.912;

        public AddressMatchingService(
            ISimilarityService similarityService,
            INormalizerService normalizerService,
            IAddressRepository repository)
        {
            _similarityService = similarityService;
            _normalizerService = normalizerService;
            _repository = repository;
        }

        public async Task<AddressMatchResponse> MatchAddressAsync(AddressInput input)
        {
            if (input == null)
            {
                return new AddressMatchResponse
                {
                    MatchType = AddressMatchType.None,
                    Score = 0.0,
                    MatchedAddress = null
                };
            }

            string inAddr1 = _normalizerService.Normalize(input.CustAddress1);
            string inAddr2 = _normalizerService.Normalize(input.CustAddress2);
            string inCity = _normalizerService.Normalize(input.CustCity);
            string inState = _normalizerService.Normalize(input.CustState);

            var dbAddresses = (await _repository.GetAllAddressesAsync()).ToList();

            if (!dbAddresses.Any())
            {
                return new AddressMatchResponse
                {
                    MatchType = AddressMatchType.None,
                    Score = 0.0,
                    MatchedAddress = null
                };
            }

            double bestScore = 0.0;
            CustomerAddress? bestMatch = null;

            foreach (var db in dbAddresses)
            {
                string dbAddr1 = _normalizerService.Normalize(db.CUST_ADDRESS1);
                string dbAddr2 = _normalizerService.Normalize(db.CUST_ADDRESS2);
                string dbCity = _normalizerService.Normalize(db.CUST_CITY);
                string dbState = _normalizerService.Normalize(db.CUST_STATE);

                double score1 = _similarityService.CalculateSimilarity(inAddr1, dbAddr1);
                double score2 = _similarityService.CalculateSimilarity(inAddr2, dbAddr2);
                double score3 = _similarityService.CalculateSimilarity(inCity, dbCity);
                double score4 = _similarityService.CalculateSimilarity(inState, dbState);

                var scores = new List<double>();
                if (!string.IsNullOrEmpty(inAddr1) && !string.IsNullOrEmpty(dbAddr1)) scores.Add(score1);
                if (!string.IsNullOrEmpty(inAddr2) && !string.IsNullOrEmpty(dbAddr2)) scores.Add(score2);
                if (!string.IsNullOrEmpty(inCity) && !string.IsNullOrEmpty(dbCity)) scores.Add(score3);
                if (!string.IsNullOrEmpty(inState) && !string.IsNullOrEmpty(dbState)) scores.Add(score4);

                double finalScore = scores.Any() ? scores.Average() : 0.0;

                if (finalScore > bestScore)
                {
                    bestScore = finalScore;
                    bestMatch = db;

                    if (finalScore >= 0.999)
                        break;
                }
            }

            if (bestScore >= EXACT_THRESHOLD)
            {
                return new AddressMatchResponse
                {
                    MatchType = AddressMatchType.Exact,
                    Score = bestScore,
                    MatchedAddress = bestMatch
                };
            }

            if (bestScore >= SIMILAR_THRESHOLD)
            {
                return new AddressMatchResponse
                {
                    MatchType = AddressMatchType.Similar,
                    Score = bestScore,
                    MatchedAddress = bestMatch
                };
            }

            return new AddressMatchResponse
            {
                MatchType = AddressMatchType.None,
                Score = bestScore,
                MatchedAddress = null
            };
        }
    }
}
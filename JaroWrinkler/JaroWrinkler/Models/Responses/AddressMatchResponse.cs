using JaroWrinklerSimilarity.Models.Enums;

namespace JaroWrinklerSimilarity.Models.Responses
{
    public class AddressMatchResponse
    {
        internal double SimilarityScore;

        public AddressMatchType MatchType { get; set; }
        public double Score { get; set; }
        public CustomerAddress? MatchedAddress { get; set; }
    }
}
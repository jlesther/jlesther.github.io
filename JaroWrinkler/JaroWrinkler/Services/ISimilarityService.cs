namespace JaroWrinklerSimilarity.Services
{
    public interface ISimilarityService
    {
        double CalculateSimilarity(string source, string target);
    }
}
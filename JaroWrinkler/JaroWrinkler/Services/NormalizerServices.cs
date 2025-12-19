using System.Globalization;
using System.Text.RegularExpressions;

namespace JaroWrinklerSimilarity.Services
{
    public class NormalizerService : INormalizerService
    {
        public string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Uppercase (culture-invariant)
            input = input.ToUpperInvariant();

            // Remove punctuation
            input = Regex.Replace(input, @"[^\w\s]", " ");

            // Normalize whitespace
            input = Regex.Replace(input, @"\s+", " ").Trim();

            // Standardize common address abbreviations
            input = Regex.Replace(input, @"\bST\b", "STREET");
            input = Regex.Replace(input, @"\bRD\b", "ROAD");
            input = Regex.Replace(input, @"\bAVE\b", "AVENUE");
            input = Regex.Replace(input, @"\bBLVD\b", "BOULEVARD");
            input = Regex.Replace(input, @"\bAPT\b", "APARTMENT");

            return input;
        }
    }
}
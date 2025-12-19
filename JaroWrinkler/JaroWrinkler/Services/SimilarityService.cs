using System;

namespace JaroWrinklerSimilarity.Services
{
    public class SimilarityService : ISimilarityService
    {
        public double CalculateSimilarity(string source, string target)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
                return 0.0;

            if (source.Equals(target, StringComparison.Ordinal))
                return 1.0;

            int s1Len = source.Length;
            int s2Len = target.Length;

            int matchDistance = Math.Max(s1Len, s2Len) / 2 - 1;

            bool[] s1Matches = new bool[s1Len];
            bool[] s2Matches = new bool[s2Len];

            int matches = 0;

            for (int i = 0; i < s1Len; i++)
            {
                int start = Math.Max(0, i - matchDistance);
                int end = Math.Min(i + matchDistance + 1, s2Len);

                for (int j = start; j < end; j++)
                {
                    if (s2Matches[j]) continue;
                    if (source[i] != target[j]) continue;

                    s1Matches[i] = true;
                    s2Matches[j] = true;
                    matches++;
                    break;
                }
            }

            if (matches == 0)
                return 0.0;

            double transpositions = 0;
            int k = 0;

            for (int i = 0; i < s1Len; i++)
            {
                if (!s1Matches[i]) continue;

                while (!s2Matches[k]) k++;

                if (source[i] != target[k])
                    transpositions++;

                k++;
            }

            double m = matches;
            double jaro =
                ((m / s1Len) +
                 (m / s2Len) +
                 ((m - transpositions / 2.0) / m)) / 3.0;

            int prefix = 0;
            int prefixLimit = Math.Min(4, Math.Min(s1Len, s2Len));

            for (int i = 0; i < prefixLimit; i++)
            {
                if (source[i] == target[i])
                    prefix++;
                else
                    break;
            }

            const double scalingFactor = 0.1;

            return jaro + (prefix * scalingFactor * (1 - jaro));
        }
    }
}